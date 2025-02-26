FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SimpleProjectStudents/SimpleProjectStudents.csproj", "SimpleProjectStudents/"]
RUN dotnet restore "SimpleProjectStudents/SimpleProjectStudents.csproj"
COPY . .
WORKDIR "/src/SimpleProjectStudents"
RUN dotnet build "SimpleProjectStudents.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleProjectStudents.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleProjectStudents.dll"]
