#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["CQRSSplitWise.Client.Query/CQRSSplitWise.Client.Query.csproj", "CQRSSplitWise.Client.Query/"]
RUN dotnet nuget add source https://nuget.pkg.github.com/mario-cale/index.json -n github -u mario-cale -p 9dbcd3716b6e24c75c423cdc47aa28a32b55ebd9 --store-password-in-clear-text
RUN dotnet restore "CQRSSplitWise.Client.Query/CQRSSplitWise.Client.Query.csproj"
COPY . .
WORKDIR "/src/CQRSSplitWise.Client.Query"
RUN dotnet build "CQRSSplitWise.Client.Query.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CQRSSplitWise.Client.Query.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CQRSSplitWise.Client.Query.dll"]