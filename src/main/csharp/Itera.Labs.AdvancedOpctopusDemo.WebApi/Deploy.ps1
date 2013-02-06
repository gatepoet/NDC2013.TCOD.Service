function Get-FrameworkDirectory()
{
    $([System.Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory())
}

function Get-Port
{
	$found = false
	$appConfig = [xml](cat .\$ServiceConfig)
	$appConfig.configuration.appSettings.add | foreach {
		if ($_.key -eq 'Port') {
			$port = $_.value
			$found = true
		}
	}
	if (!$found) $port = "8080"
	return $port
}

function Install-WebApi-Service
{
	$service = Get-Service $ServiceName -ErrorAction SilentlyContinue

	$fullPath = Resolve-Path $ServiceExecutable

	if (! $service)
	{
		Write-Host "Installing service"

		$frameworkDir = Get-FrameworkDirectory
		Set-Alias install_util (Join-Path $frameworkDir "installutil.exe")
		install_util $fullPath | Write-Host   
	}
	else
	{
		Write-Host "The service will be stopped and reconfigured"

		Stop-Service $ServiceName -Force
		& "sc.exe" config $service.Name binPath= $fullPath start= auto | Write-Host
	}

	
}
function Set-User-Rights
{
	$port = Get-Port
	$user = "NT AUTHORITY\NETWORK SERVICE"
	if (netsh http show urlacl url=http://+:$port/ | find.exe $user) {
	    Write-Host "Setting rights for '$user' on $port

		netsh http add urlacl url=http://+:$port/ user=$user | Write-Host 
	}
	else {
	    Write-Host Rights already set for '$user' on port $port.
	}
}
$ServiceName = "Itera.Labs.AdvancedOpctopusDemo.WebApi"
$ServiceExecutable = $ServiceName + ".exe"
$ServiceConfig = $ServiceExecutable + ".config"

Install-WebApi-Service
Set-User-Rights
Write-Host "Starting service"
Start-Service $ServiceName | Write-Host
