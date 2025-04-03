# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY *.sln .
COPY CarProject/*.csproj ./CarProject/
COPY CarProject.Core/*.csproj ./CarProject.Core/
COPY CarProject.Data/*.csproj ./CarProject.Data/
COPY CarProject.ApplicationServices/*.csproj ./CarProject.ApplicationServices/
COPY CarProject.Test/*.csproj ./CarProject.Test/

# Restore dependencies
RUN dotnet restore

# Copy all files and build
COPY . .
RUN dotnet publish -c Release -o /app

# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app .

# Expose port and set entrypoint
EXPOSE 80
ENTRYPOINT ["dotnet", "CarProject.dll"]

