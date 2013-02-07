function Get-FrameworkDirectory
{
    $([System.Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory())
}

function Get-Port($config)
{
	Write-Host "Reading config file '$config'"
	$found = $FALSE
	$appConfig = [xml](cat .\$config)
	$appConfig.configuration.appSettings.add | foreach {
		if ($_.key -eq 'Port') {
			$port = $_.value
			$found = $TRUE
		}
	}
	if (-not $found) {$port = "8080"}
	$($port)
}
function Install-WebApi-Service($name, $executable)
{
	Write-Host "'$name'$executable'"
	$service = Get-Service $name -ErrorAction SilentlyContinue

	$fullPath = Resolve-Path $executable

	if (! $service)
	{
		Write-Host "Installing service"

		$frameworkDir = Get-FrameworkDirectory
		Set-Alias install_util (Join-Path $frameworkDir "installutil.exe")
		install_util $fullPath | Write-Host  
		$service = Get-Service $name 
		Write-Host $service.User
	}
	else
	{
		Write-Host "The service will be stopped and reconfigured"

		Stop-Service $name -Force
		& "sc.exe" config $service.Name binPath= $fullPath start= auto | Write-Host
	}
	$config = $executable + ".config"
	$port = Get-Port $config
	Set-User-Rights $port "NT AUTHORITY\NETWORK SERVICE" 

	Write-Host "Starting service"
	Start-Service $name | Write-Host
	
}

function Set-User-Rights($port, $user)
{
	$uri = "http://+:$port/"
	$acls = (netsh http show urlacl url=$uri | Select-String -SimpleMatch $user)
	if ($acls.count -eq 0 ) {
	    Write-Host "Setting rights for '$user' on $uri"

		netsh http add urlacl url=$uri user=$user | Write-Host 
	}
	else {
	    Write-Host "Rights already set for '$user' on $uri."
	}
}
function Uninstall-WebApi-Service
{
	$service = Get-Service $ServiceName -ErrorAction SilentlyContinue

	$fullPath = Resolve-Path $ServiceExecutable

	if (! $service)
	{
		Write-Host "The service is not installed. No need to uninstall."
	}
	else
	{
		Write-Host "Uninstalling service"

		$frameworkDir = Get-FrameworkDirectory
		Set-Alias install_util (Join-Path $frameworkDir "installutil.exe")
		install_util -u $fullPath | Write-Host   
	}

	Get-Port | Remove-User-Rights
}
function Remove-User-Rights($port)
{
	$user = "NT AUTHORITY\NETWORK SERVICE"
	if (netsh http show urlacl url=http://+:$port/ | find.exe $user) {
	    Write-Host "Removing rights for '$user' on $port"

		netsh http delete urlacl url=http://+:$port/ | Write-Host 
	}
	else {
	    Write-Host "Rights not set for '$user' on port $port. No need to remove."
	}
}

