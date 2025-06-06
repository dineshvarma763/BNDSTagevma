#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Project/Bnd.Api/Bnd.Api.csproj", "Project/Bnd.Api/"]
COPY ["Features/Example/Bnd.Example/Bnd.Example.csproj", "Features/Example/Bnd.Example/"]
COPY ["Foundation/Common/Bnd.Common/Bnd.Common.csproj", "Foundation/Common/Bnd.Common/"]
RUN dotnet restore "Project/Bnd.Api/Bnd.Api.csproj"
COPY . .
WORKDIR "/src/Project/Bnd.Api"
RUN dotnet build "Bnd.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bnd.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bnd.Api.dll"]