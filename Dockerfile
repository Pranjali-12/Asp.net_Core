# Use .NET 9 SDK for build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy all files and build
COPY . ./
RUN dotnet publish -c Release -o out

# Use .NET 9 runtime for final image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose port
EXPOSE 8080
ENTRYPOINT ["dotnet", "dotnet crud.dll"]
