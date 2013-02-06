function Get-FrameworkDirectory()
{
    $([System.Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory())
}

function Install-WebApi-Service
{
    Write-Host "Create the ASPNETDB SQL Server database for the membership system"
    $frameworkDir = Get-FrameworkDirectory
    Set-Alias install_util (Join-Path $frameworkDir "installutil.exe")
    install_util Itera.Labs.AdvancedOpctopusDemo.WebApi.exe | Write-Host   

	netsh http add urlacl url=http://+:$Env:Port/ user="NT AUTHORITY\NETWORK SERVICE"
}
Install-WebApi-Service
Start-Service Itera.Labs.AdvancedOpctopusDemo.WebApi | Write-Host