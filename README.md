# README #

This README details the steps necessary to get your application up and running.

### What is this repository for? ###

* B&D Australia's Umbraco 9 CMS and its API Endpoint implementation
* v1.0

### Requirements ###
* Visual Studio 2019
* SQL Server 2017+
* SSMS
* .NET Core 2.x+ SDK
* IIS

### How do I get set up? ###

* Create a database called bnd_umbraco using SSMS
* Ensure that your sa user is the default Sitback Solutions one and uses the same credentials
* Request for a database backup from Titu or Ez (or use the one in this repository)
* Create a new website in IIS and name is local.api.bnd.sitback.com.au
* Ensure that the website is running as "No Managed Code"
* Ensure your application pool has access to the repository folder
* Target the physical folder to [Your project folder]\bnd-umbraco-api\src\Project\Bnd.Api
* Add the hostname to your host entry in Windows System32
* Rebuild the solution
* Navigate to http(s)://local.api.bnd.sitback.com.au

### Contribution guidelines ###

* Ensure that any features created is bound by its domain and sit in the appropriate folder structure
* Ensure that the endpoints have meaningful names
* Ensure that you use dependency resolution for new features

### Who do I talk to? ###

* Imon, Titu, Ez and Lindsey
