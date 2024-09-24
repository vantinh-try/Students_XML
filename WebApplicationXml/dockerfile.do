# Sử dụng hình ảnh base cho ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Sử dụng hình ảnh cho việc xây dựng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebApplicationXml/WebApplicationXml.csproj", "WebApplicationXml/"]
RUN dotnet restore "WebApplicationXml/WebApplicationXml.csproj"
COPY . .
WORKDIR "/src/WebApplicationXml"
RUN dotnet build "WebApplicationXml.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplicationXml.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplicationXml.dll"]
