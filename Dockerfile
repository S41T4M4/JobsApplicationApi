# Etapa 1: Imagem base de construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia os arquivos publicados (a pasta que você já tem)
COPY . ./

# Restaura as dependências e publica a aplicação
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Etapa 2: Imagem final para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar os arquivos da etapa de construção para a imagem final
COPY --from=build-env /app/out .

# Expor a porta que a aplicação vai rodar (normalmente 80 ou 5000)
EXPOSE 80

# Comando de entrada para rodar o app
ENTRYPOINT ["dotnet", "JobApplication.dll"]
