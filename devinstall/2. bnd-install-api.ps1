Set-ExecutionPolicy Unrestricted
##############################################
#
#                 Variables
#       Change these variables if you put 
#          your project somewhere else
#
##############################################
$sqlPackagePath = "C:\Program Files\Microsoft SQL Server\160\DAC\bin\"
$sqlServerName = "WIN-11-PG"
$projectPath = "C:\Projects\BND Australia\bnd-umbraco-api\src\Project\Bnd.Api"
$apiCertificatePath = "C:\Projects\BND Australia\bnd-umbraco-api\devinstall\certificates\bnd_API_Cert.pfx"
$rootCertificatePath = "C:\Projects\BND Australia\bnd-umbraco-api\devinstall\certificates\bnd_Root_Cert.pfx"
$dbBackupPath = "C:\Projects\BND Australia\bnd-umbraco-api\devinstall\db\bnd_umbraco.bacpac"
$rootCertificateThumb = "35f8b3d303bf462249cfd8dae9bbea21027326a0"
$apiCertificateThumb = "52195280a50a6800532fe6af3472323eade1c830"
$apiAppPool = "local.api.bnd.sitback.com.au"
$sslFlags = 1

##############################################
#
#
#          Checks for path, wont run  
#           if path cannot be found
#
##############################################
if(Test-Path -Path $projectPath)
{
    Write-Host "Found the project folder, lets get started!" -ForegroundColor Green
}
else 
{
    Write-Host "$projectPath is not a valid path to your API project." -ForegroundColor DarkYellow
    Start-Sleep -Seconds 10
    Exit
}

if(Test-Path -Path $rootCertificatePath)
{
    Write-Host "Found the Root Certificate" -ForegroundColor Green
}
else 
{
    Write-Host "Uh oh! $rootCertificatePath is missing from its location" -ForegroundColor DarkYellow
    Start-Sleep -Seconds 10
    Exit
}

if(Test-Path -Path $apiCertificatePath)
{
    Write-Host "Found the API Certificate" -ForegroundColor Green
}
else 
{
    Write-Host "Uh oh! $apiCertificatePath is missing from its location" -ForegroundColor DarkYellow
    Start-Sleep -Seconds 10
    Exit
}

###########################
#
#  Restore the development database
#
##########################
Write-Host "Restoring database(s)" -ForegroundColor DarkYellow
Write-Host "....." -ForegroundColor DarkYellow
if((Test-Path -Path $sqlPackagePath) -AND (Test-Path -Path $dbBackupPath))
{
    Write-Host "Found SQL Package" -ForegroundColor Green
    Set-Location $sqlPackagePath    
    .\SqlPackage.exe /Action:Import /sf:"$dbBackupPath" /tsn:$sqlServerName /tdn:bnd_umbraco /tu:sa /tp:Abcd!234 
    
    Write-Host "Database restored." -ForegroundColor Green
}
else
{
    Write-Host "Unable to fine $sqlPackagePath, but we will continue. You will need to manually restore your database" -ForegroundColor Yellow
}

################################
#
#  Installing SSL Certificate (Manual intervention)
#
################################
$certPassword = Get-Credential -UserName 'Enter the certificate password' -Message 'Enter certificate password'
#Import root certificate
if (Get-ChildItem -Path Cert:\LocalMachine\Root | Where-Object {$_.Thumbprint -eq $rootCertificateThumb})
{
    Write-Host "The root certificate is already installed" -ForegroundColor Yellow
}
else
{
    Import-PfxCertificate -FilePath $rootCertificatePath -CertStoreLocation Cert:\LocalMachine\Root -Password $certPassword.Password
}

#import client cert
if (Get-ChildItem -Path Cert:\LocalMachine\My | Where-Object {$_.Thumbprint -eq $apiCertificateThumb}) 
{
    Write-Host "The API certificate is already installed" -ForegroundColor Yellow
}
else
{
    Import-PfxCertificate -FilePath $apiCertificatePath -CertStoreLocation Cert:\LocalMachine\My -Password $certPassword.Password
}

################################
#
#  Setting up IIS
#
################################
Import-Module WebAdministration
Import-Module IISAdministration

Write-Host "Now that's done.... Let's start by setting up the API site" -ForegroundColor Green
Write-Host "Creating $apiAppPool application pool and website..." -ForegroundColor Green

# Create App Pool
if(get-website | where-object { $_.name -eq $apiAppPool })
{
    Write-Host "$apiAppPool already exists...skipping website creation..." -ForegroundColor DarkYellow
}
else 
{
    New-WebAppPool -Name $apiAppPool
    $apiSite = New-IISSite -Name $apiAppPool -PhysicalPath $projectPath -BindingInformation "*:80:$apiAppPool"
    Start-Sleep -Seconds 5    
    Start-Sleep -Seconds 5
    Set-ItemProperty -Path IIS:\AppPools\$apiAppPool managedRuntimeVersion ""
    Set-ItemProperty -Path IIS:\Sites\$apiAppPool applicationPool $apiAppPool
}

# Add HTTPS binding
New-WebBinding -Name $apiAppPool -IPAddress "*" -Port 443 -HostHeader $apiAppPool -Protocol "https" -SslFlags $sslFlags
# Add SSL Cert to the binding
(Get-WebBinding -Name $apiAppPool -Port 443 -Protocol "https").AddSslCertificate($apiCertificateThumb, "my")


#Start the app pool
Start-WebAppPool -Name $apiAppPool
##########################################
#
#
#      Updating Host File 
#
#
##########################################
Write-Host "Updating Host files" -ForegroundColor Green
$HostFile = 'C:\Windows\System32\drivers\etc\hosts'
 
# Create a backup copy of the Hosts file
$dateFormat = (Get-Date).ToString('dd-MM-yyyy hh-mm-ss')
$FileCopy = $HostFile + '.' + $dateFormat  + '.copy'
Copy-Item $HostFile -Destination $FileCopy
 
#Hosts to Add
$Hosts = @("$apiAppPool")
 
# Get the contents of the Hosts file
$File = Get-Content $HostFile
 
# write the Entries to hosts file, if it doesn't exist.
foreach ($HostFileEntry in $Hosts)
{
    Write-Host "Checking existing HOST file entries for $HostFileEntry..."
     
    #Set a Flag
    $EntryExists = $false
     
    if ($File -contains "127.0.0.1 `t $HostFileEntry")
    {
        Write-Host "Host File Entry for $HostFileEntry is already exists." -ForegroundColor Yellow
        $EntryExists = $true
    }
    #Add Entry to Host File
    if (!$EntryExists)
    {
        Write-host "Adding Host File Entry for $HostFileEntry" -ForegroundColor Green
        Add-content -path $HostFile -value "127.0.0.1 `t $HostFileEntry"
    }
}

###########################
#
#  Give IIS app pool permission to folder
#
##########################
$accessRule = New-Object System.Security.AccessControl.FileSystemAccessRule("IIS AppPool\$apiAppPool", "FullControl", "ContainerInherit,ObjectInherit", "None", "Allow")
$acl = Get-ACL $projectPath
$acl.AddAccessRule($accessRule)
Set-ACL -Path $projectPath -ACLObject $acl

$accessRuleIIS = New-Object System.Security.AccessControl.FileSystemAccessRule("IIS_IUSRS", "FullControl", "ContainerInherit,ObjectInherit", "None", "Allow")
$aclIIS = Get-ACL $projectPath
$aclIIS.AddAccessRule($accessRuleIIS)
Set-ACL -Path $projectPath -ACLObject $aclIIS

###########################
#
#  Build the solution
#
##########################
Set-Location $projectPath
Write-Host "Building the solution in debug mode" -ForegroundColor DarkYellow
dotnet build -c Debug
Write-Host "Building the solution in release mode" -ForegroundColor DarkYellow
dotnet build -c Release
Write-Host "Finished building..." -ForegroundColor Green
Write-Host "..........................................." -ForegroundColor DarkYellow

###########################
#
#  Navigate to site
#
##########################
Write-Host "Launching website in Chrome........." -ForegroundColor DarkYellow
Start-Sleep 10
[system.Diagnostics.Process]::Start("chrome","https://$apiAppPool")