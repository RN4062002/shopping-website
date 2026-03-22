# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["ecomServer.csproj", "."]
RUN dotnet restore "./ecomServer.csproj"

# Copy the rest of the code and build the app
COPY . .
RUN dotnet build "ecomServer.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "ecomServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the ASP.NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose the port the app listens on
# Render will automatically detect this or you can set the PORT env var
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "ecomServer.dll"]
