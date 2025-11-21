#!/bin/bash
set -e

RG="workwell-rg"
LOCATION="brazilsouth"
SQLSERVER="workwellsqlserver$RANDOM"
SQLADMIN="workwelladmin"
SQLPASSWORD="SenhaForte!234"
SQLDB="workwell"
APPPLAN="workwell-plan"
WEBAPP="workwell-app"
ACR="workwellacr$RANDOM"

echo "== [1] Resource Group =="
az group create --name "$RG" --location "$LOCATION"

echo "== [2] Azure SQL Server =="
az sql server create --name "$SQLSERVER" --resource-group "$RG" --location "$LOCATION" --admin-user "$SQLADMIN" --admin-password "$SQLPASSWORD"

echo "== [3] Azure SQL Database =="
az sql db create --name "$SQLDB" --resource-group "$RG" --server "$SQLSERVER" --edition GeneralPurpose --service-objective GP_S_Gen5_2

echo "== [4] App Service Plan (Linux) =="
az appservice plan create --name "$APPPLAN" --resource-group "$RG" --is-linux --sku B1 --location "$LOCATION"

echo "== [5] Azure Container Registry =="
az acr create --resource-group "$RG" --name "$ACR" --sku Basic

echo "== [6] Web App (Container) =="
az webapp create --resource-group "$RG" --plan "$APPPLAN" --name "$WEBAPP" --deployment-container-image-name "$ACR.azurecr.io/workwell-api:latest"

echo "== [7] AppSettings: ConnectionString/ApiKeys =="
CONNECTIONSTRING="Server=tcp:$SQLSERVER.database.windows.net,1433;Initial Catalog=$SQLDB;Persist Security Info=False;User ID=$SQLADMIN;Password=$SQLPASSWORD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
az webapp config appsettings set --resource-group "$RG" --name "$WEBAPP" --settings \
  "ConnectionStrings__DefaultConnection=$CONNECTIONSTRING" \
  "ApiKeys__Admin=admin-api-key" \
  "ApiKeys__RH=rh-api-key" \
  "ApiKeys__Psicologo=psicologo-api-key" \
  "ApiKeys__Funcionario=funcionario-api-key" \
  "SuperApiKey=super-api-key"

echo "== Infraestrutura pronta! Consulte o portal Azure."