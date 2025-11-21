#!/bin/bash
set -e

# Script de provisionamento de infraestrutura Azure para WorkWell API
# Usa PaaS (Web App + Azure SQL) - SEM containerização

RG="workwell-rg"
LOCATION="brazilsouth"
SQLSERVER="workwellsqlserver$RANDOM"
SQLADMIN="workwelladmin"
SQLPASSWORD="SenhaForte!234"
SQLDB="workwell"
APPPLAN="workwell-plan"
WEBAPP="workwell-app"

echo "== [1] Resource Group =="
az group create --name "$RG" --location "$LOCATION"

echo "== [2] Azure SQL Server =="
az sql server create --name "$SQLSERVER" --resource-group "$RG" --location "$LOCATION" --admin-user "$SQLADMIN" --admin-password "$SQLPASSWORD"

echo "== [3] Azure SQL Database (PaaS) =="
az sql db create --name "$SQLDB" --resource-group "$RG" --server "$SQLSERVER" --edition GeneralPurpose --service-objective GP_S_Gen5_2

echo "== [4] Configurar Firewall do SQL Server (permitir serviços Azure) =="
az sql server firewall-rule create --resource-group "$RG" --server "$SQLSERVER" --name "AllowAzureServices" --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0

echo "== [5] App Service Plan (Linux - PaaS) =="
az appservice plan create --name "$APPPLAN" --resource-group "$RG" --is-linux --sku B1 --location "$LOCATION"

echo "== [6] Web App (PaaS - .NET 8.0) =="
az webapp create --resource-group "$RG" --plan "$APPPLAN" --name "$WEBAPP" --runtime "DOTNETCORE:8.0"

echo "== [7] Configurar AppSettings: ConnectionString/ApiKeys =="
CONNECTIONSTRING="Server=tcp:$SQLSERVER.database.windows.net,1433;Initial Catalog=$SQLDB;Persist Security Info=False;User ID=$SQLADMIN;Password=$SQLPASSWORD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
az webapp config appsettings set --resource-group "$RG" --name "$WEBAPP" --settings \
  "ConnectionStrings__DefaultConnection=$CONNECTIONSTRING" \
  "ApiKeys__Admin=admin-api-key" \
  "ApiKeys__RH=rh-api-key" \
  "ApiKeys__Psicologo=psicologo-api-key" \
  "ApiKeys__Funcionario=funcionario-api-key" \
  "SuperApiKey=super-api-key"

echo "== [8] Habilitar logs =="
az webapp log config --resource-group "$RG" --name "$WEBAPP" --application-logging filesystem --level information

echo "== Infraestrutura PaaS criada com sucesso! =="
echo "Resource Group: $RG"
echo "SQL Server: $SQLSERVER.database.windows.net"
echo "SQL Database: $SQLDB"
echo "Web App: https://$WEBAPP.azurewebsites.net"
echo ""
echo "IMPORTANTE: Configure as variáveis secretas no Azure DevOps antes de executar as pipelines!"