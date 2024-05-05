#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 4200
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OpenFinan.WebApi/OpenFinan.WebApi.csproj", "OpenFinan.WebApi/"]
COPY ["OpenFinan.Domain/OpenFinan.Domain.csproj", "OpenFinan.Domain/"]
COPY ["OpenFinan.DomainBase.Exceptions/OpenFinan.DomainBase.Exceptions.csproj", "OpenFinan.DomainBase.Exceptions/"]
COPY ["OpenFinan.Infra.Repository/OpenFinan.Infra.Repository.csproj", "OpenFinan.Infra.Repository/"]
COPY ["OpenFinan.Application/OpenFinan.Application.csproj", "OpenFinan.Application/"]
RUN dotnet restore "OpenFinan.WebApi/OpenFinan.WebApi.csproj"
COPY . .
WORKDIR "/src/OpenFinan.WebApi"
RUN dotnet build "OpenFinan.WebApi.csproj" -c Release -o /src/app/build

FROM build AS publish
RUN dotnet publish "OpenFinan.WebApi.csproj" -c Release -o /src/app/publish

FROM base AS final
WORKDIR /src/app
COPY --from=publish /src/app/publish .
ENTRYPOINT ["dotnet", "OpenFinan.WebApi.dll"]
