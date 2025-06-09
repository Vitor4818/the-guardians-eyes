FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.sln .
COPY TheGuardiansEyesApi/*.csproj ./TheGuardiansEyesApi/
COPY TheGuardiansEyesBusiness/*.csproj ./TheGuardiansEyesBusiness/
COPY TheGuardiansEyesData/*.csproj ./TheGuardiansEyesData/
COPY TheGuardiansEyesModel/*.csproj ./TheGuardiansEyesModel/
COPY TheGuardiansEyesMVC/*.csproj ./TheGuardiansEyesMVC/

COPY . .

RUN dotnet restore

RUN dotnet publish TheGuardiansEyesApi/TheGuardiansEyesApi.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
RUN useradd -m appuser
USER appuser
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5193

COPY --from=build /app/publish .

EXPOSE 5193

ENTRYPOINT ["dotnet", "TheGuardiansEyesApi.dll"]