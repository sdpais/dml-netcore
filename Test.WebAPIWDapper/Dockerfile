#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 9200
EXPOSE 9201

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["Test.WebAPIWDapper/Test.WebAPIWDapper.csproj", "Test.WebAPIWDapper/"]
RUN dotnet restore "./Test.WebAPIWDapper/Test.WebAPIWDapper.csproj"
RUN ls
COPY . .
WORKDIR "/src/Test.WebAPIWDapper"
RUN ls
RUN dotnet build "./Test.WebAPIWDapper.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./Test.WebAPIWDapper.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://*:9201
ENTRYPOINT ["dotnet", "Test.WebAPIWDapper.dll"]