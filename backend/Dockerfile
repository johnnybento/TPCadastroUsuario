
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY TPCadastroUsuario.sln ./
COPY src/Adapters/Drivers/WebApi/TPCadastroUsuario.Adapters.Drivers.WebApi.csproj src/Adapters/Drivers/WebApi/
COPY src/Application/TPCadastroUsuario.Application.csproj src/Application/
COPY src/Core/TPCadastroUsuario.Core.csproj src/Core/
COPY src/Adapters/Driven/Infrastructure/TPCadastroUsuario.Adapters.Driven.Infrastructure.csproj src/Adapters/Driven/Infrastructure/


RUN dotnet restore src/Adapters/Drivers/WebApi/TPCadastroUsuario.Adapters.Drivers.WebApi.csproj


COPY . .


RUN dotnet test \
      src/Tests/Application.Tests/TPCadastroUsuario.Application.Tests.csproj \
      --no-restore --no-build -c Release \
      --logger \"console;verbosity=detailed\"


WORKDIR /src/src/Adapters/Drivers/WebApi
RUN dotnet publish -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .


ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80
ENTRYPOINT ["dotnet","TPCadastroUsuario.Adapters.Drivers.WebApi.dll"]
