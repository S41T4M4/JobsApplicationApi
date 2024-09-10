# Etapa 1: Imagem base de constru��o
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copia os arquivos publicados (a pasta que voc� j� tem)
COPY . ./

# Etapa 2: Imagem final para execu��o
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copiar os arquivos da etapa de constru��o para a imagem final
COPY --from=build-env /app .

# Expor a porta que a aplica��o vai rodar (normalmente 80 ou 5000)
EXPOSE 80

# Comando de entrada para rodar o app
ENTRYPOINT ["dotnet", "JobApplication.dll"]
