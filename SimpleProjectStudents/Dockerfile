# Use the official .NET image as a base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the solution and project files
COPY ["SimpleProjectStudents.sln", "./"]
COPY ["SimpleProjectStudents/SimpleProjectStudents.csproj", "SimpleProjectStudents/"]

# Restore dependencies
RUN dotnet restore "SimpleProjectStudents.sln"

# Copy everything and build
COPY . .
WORKDIR "/src/SimpleProjectStudents"
RUN dotnet build --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-build -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleProjectStudents.dll"]
