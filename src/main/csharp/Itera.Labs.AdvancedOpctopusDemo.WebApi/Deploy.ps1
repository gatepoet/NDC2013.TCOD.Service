function Get-FrameworkDirectory()
{
    $([System.Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory())
}

function Install-WebApi-Service
{
    Write-Host "Installing service"
    $frameworkDir = Get-FrameworkDirectory
    Set-Alias install_util (Join-Path $frameworkDir "installutil.exe")
    install_util Itera.Labs.AdvancedOpctopusDemo.WebApi.exe | Write-Host   

	$port = $OctopusParameters["Port"];
    Write-Host "Setting rights on http://+:$port/"
	netsh http add urlacl url=http://+:$port/ user="NT AUTHORITY\NETWORK SERVICE" | Write-Host 
}
Install-WebApi-Service
Write-Host "Starting service"
Start-Service Itera.Labs.AdvancedOpctopusDemo.WebApi | Write-Host