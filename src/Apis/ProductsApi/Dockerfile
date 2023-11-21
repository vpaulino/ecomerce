#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Apis/Directory.Build.props", "src/Apis/"]
COPY ["src/Apis/ProductsApi/ProductsApi.csproj", "src/Apis/ProductsApi/"]
COPY ["src/Apis/ApiExtensions/ApiExtensions.csproj", "src/Apis/ApiExtensions/"]
RUN dotnet restore "./src/Apis/ProductsApi/./ProductsApi.csproj"
COPY . .
WORKDIR "/src/src/Apis/ProductsApi"
RUN dotnet build "./ProductsApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductsApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


RUN dotnet dev-certs https -ep /https/aspnetapp.pfx -p {password}
RUN dotnet dev-certs https --clean --import /https/aspnetapp.pfx -p {password}


ENTRYPOINT ["dotnet", "ProductsApi.dll"]