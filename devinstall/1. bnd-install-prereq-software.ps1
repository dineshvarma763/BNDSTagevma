###################################
#
#    IIS And Windows configuration
#
###################################
Set-ExecutionPolicy Bypass -Scope Process

Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebServerRole
Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebServer
Enable-WindowsOptionalFeature -Online -FeatureName IIS-CommonHttpFeatures
Enable-WindowsOptionalFeature -Online -FeatureName IIS-HttpErrors
Enable-WindowsOptionalFeature -Online -FeatureName IIS-HttpRedirect
Enable-WindowsOptionalFeature -Online -FeatureName IIS-ApplicationDevelopment

Enable-WindowsOptionalFeature -online -FeatureName NetFx4Extended-ASPNET45
Enable-WindowsOptionalFeature -Online -FeatureName IIS-NetFxExtensibility45

Enable-WindowsOptionalFeature -Online -FeatureName IIS-HealthAndDiagnostics
Enable-WindowsOptionalFeature -Online -FeatureName IIS-HttpLogging
Enable-WindowsOptionalFeature -Online -FeatureName IIS-LoggingLibraries
Enable-WindowsOptionalFeature -Online -FeatureName IIS-RequestMonitor
Enable-WindowsOptionalFeature -Online -FeatureName IIS-HttpTracing
Enable-WindowsOptionalFeature -Online -FeatureName IIS-Security
Enable-WindowsOptionalFeature -Online -FeatureName IIS-RequestFiltering
Enable-WindowsOptionalFeature -Online -FeatureName IIS-Performance
Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebServerManagementTools
Enable-WindowsOptionalFeature -Online -FeatureName IIS-IIS6ManagementCompatibility
Enable-WindowsOptionalFeature -Online -FeatureName IIS-Metabase
Enable-WindowsOptionalFeature -Online -FeatureName IIS-ManagementConsole
Enable-WindowsOptionalFeature -Online -FeatureName IIS-BasicAuthentication
Enable-WindowsOptionalFeature -Online -FeatureName IIS-WindowsAuthentication
Enable-WindowsOptionalFeature -Online -FeatureName IIS-StaticContent
Enable-WindowsOptionalFeature -Online -FeatureName IIS-DefaultDocument
Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebSockets
Enable-WindowsOptionalFeature -Online -FeatureName IIS-ApplicationInit
Enable-WindowsOptionalFeature -Online -FeatureName IIS-ISAPIExtensions
Enable-WindowsOptionalFeature -Online -FeatureName IIS-ISAPIFilter
Enable-WindowsOptionalFeature -Online -FeatureName IIS-HttpCompressionStatic

Enable-WindowsOptionalFeature -Online -FeatureName IIS-ASPNET45



##############################################
#
#
#          Prerequisites 
#
#
##############################################

# 1.Install Chocolatey
Write-Host "Setting Execution Policy and Installing Chocolatey" -ForegroundColor Green
Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))

# 2. Set chocolatey to auto accept
Write-Host "Enabling global confirmation for Chocolatey" -ForegroundColor Green
choco feature enable -n allowGlobalConfirmation

Write-Host "Installing all required software. Might want to go grab a cup of coffee and some biscuits..." -ForegroundColor Green

# 3. Install Chrome
Write-Host "Installing Chrome" -ForegroundColor Green
choco install googlechrome

# 4. Install Visual Studio 2022
Write-Host "Installing VS 2022 Community" -ForegroundColor Green
choco install visualstudio2022community --version 117.1.6.0 -y

# 5. Install .NET 6 Hosting Bundle
Write-Host "Installing .NET 6 Hosting Bundle" -ForegroundColor Green
choco install dotnet-windowshosting --version 6.0.5 -y

# 6. Install SQL Server 2017
Write-Host "Installing SQL Server 2017 Developer edition" -ForegroundColor Green
choco install sql-server-2017 --version 14.0.1000 -y

# 7. Install VSCode
Write-Host "Installing VS Code" -ForegroundColor Green
choco install vscode --version 1.67.1 -y

# 8. Installing SQL Server Management Studio
Write-Host "Installing SSMS" -ForegroundColor Green
choco install sql-server-management-studio --version 15.0.18410.0 -y

# 9. Install SQL Server Data Tools
Write-Host "Installing SSDT" - ForegroundColor Green
choco install ssdt17 -y

# 10. .NET Core 2.2 Hosting Bundle (just in case)
Write-Host "Installing .NET Core 2.2 Hosting Bundle" -ForegroundColor Green
choco install dotnetcore-2.2-windowshosting --version 2.2.8 -y

# 11. .NET SDK
Write-Host "Installing .NET 6 SDK" -ForegroundColor Green
choco install dotnet-sdk --version 6.0.300 -y

# 12. SSDT
Write-Host "Installing SSDT" -ForegroundColor Green
choco install ssdt17 --version 14.0.16248.0 -y

# 13. DacFX
Write-Host "Installing DacFX" -ForegroundColor Green
choco install dacfx-18 --version 15.0.4316.1 -y

# 14. Url Rewrite and web deploy
Write-Host "Installing WebDeploy and URL Rewrite" -ForegroundColor Green
choco install webdeploy -y
choco install urlrewrite -y

###############################################
#
#   Disabling TLS 1.3 and enabling TLS 1.2
#
###############################################
Write-Host "Enabling TLS 1.2" -ForegroundColor DarkYellow
New-Item 'HKLM:\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.2\Server' -Force | Out-Null
    
New-ItemProperty -path 'HKLM:\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.2\Server' -name 'Enabled' -value '1' -PropertyType 'DWord' -Force | Out-Null
    
New-ItemProperty -path 'HKLM:\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.2\Server' -name 'DisabledByDefault' -value 0 -PropertyType 'DWord' -Force | Out-Null
    
New-Item 'HKLM:\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.2\Client' -Force | Out-Null
    
New-ItemProperty -path 'HKLM:\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.2\Client' -name 'Enabled' -value '1' -PropertyType 'DWord' -Force | Out-Null
    
New-ItemProperty -path 'HKLM:\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.2\Client' -name 'DisabledByDefault' -value 0 -PropertyType 'DWord' -Force | Out-Null
 
Write-Host 'TLS 1.2 has been enabled.' -ForegroundColor Green

Write-Host "Development prerequisites finished installing.." -ForegroundColor Green