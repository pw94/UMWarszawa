# remove all resources from Resource Group at Azure Subscription
# params: name of resource group
# params: list of resource types to remove (default Azure Web App and Azure SQL)
param (
    [string]$ResourceGroup,
    [string[]]$ResourcesTypes = @("Microsoft.Web/sites", "Microsoft.Sql/servers/databases")
)

foreach ($type in $ResourcesTypes) {
    $resources = Get-AzureRmResource -ResourceGroupName $ResourceGroup -ResourceType $type
    if ($resources) {
        foreach ($resource in $resources) {
            $result = Remove-AzureRmResource -ResourceId $resource.ResourceId -Force
            if ($result) {
                Write-Output $resource
            }
        }
    }
}
