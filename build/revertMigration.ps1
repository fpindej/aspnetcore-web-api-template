param(
    [string]$ProjectPath = "..\src\Infrastructure.SqlServer\Infrastructure.SqlServer.csproj",
    [string]$StartupProjectPath = "..\src\Api\Api.csproj"
)

function ExecuteCommand {
    param(
        [string]$Command
    )

    try {
        Write-Host "Executing: $Command"
        Invoke-Expression $Command
        if ($LASTEXITCODE -ne 0) {
            throw "Command exited with code $LASTEXITCODE"
        }
    } catch {
        Write-Host "Error: $_"
        exit $LASTEXITCODE
    }
}

Write-Host "Reverting the last migration..."

ExecuteCommand "dotnet ef migrations remove --project $ProjectPath --startup-project $StartupProjectPath"

Write-Host "Last migration successfully reverted."