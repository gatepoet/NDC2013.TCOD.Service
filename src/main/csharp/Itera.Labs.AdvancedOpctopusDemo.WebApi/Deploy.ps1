function Get-FrameworkDirectory()
{
    $([System.Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory())
}

function Create-SQLASPNETMembership
{
    Write-Host "Create the ASPNETDB SQL Server database for the membership system"
    $frameworkDir = Get-FrameworkDirectory
    Set-Alias install_util (Join-Path $frameworkDir "installutil.exe")
    install_util Itera.Labs.AdvancedOpctopusDemo.WebApi.exe    
}
Create-SQLASPNETMembership