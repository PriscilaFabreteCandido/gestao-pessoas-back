FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG PROJECT_FOLDER=kovr_portal_sinistro_back
WORKDIR /src

COPY ${PROJECT_FOLDER}/*.csproj ./
RUN dotnet restore

COPY ${PROJECT_FOLDER}/ ./
RUN dotnet publish -c Release -o /app/publish -p:AssemblyName="Application"

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

RUN apt-get update && apt-get install -y fonts-dejavu-core fonts-liberation

ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Application.dll"]