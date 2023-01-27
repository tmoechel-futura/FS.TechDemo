FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FS.TechDemo.OrderService/FS.TechDemo.OrderService.csproj", "FS.TechDemo.OrderService/"]
COPY ["FS.TechDemo.Shared/FS.TechDemo.Shared.csproj", "FS.TechDemo.Shared/"]
RUN dotnet restore "FS.TechDemo.OrderService/FS.TechDemo.OrderService.csproj"
COPY . .
WORKDIR "/src/FS.TechDemo.OrderService"
RUN dotnet build "FS.TechDemo.OrderService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FS.TechDemo.OrderService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FS.TechDemo.OrderService.dll"]
