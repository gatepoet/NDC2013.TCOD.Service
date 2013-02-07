. .\DeployUtil.ps1

$ServiceName = "Itera.Labs.AdvancedOpctopusDemo.WebApi"
$ServiceExecutable = $ServiceName + ".exe"

Install-WebApi-Service $ServiceName $ServiceExecutable
