# Sử dụng hình ảnh .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Sao chép csproj và khôi phục các phụ thuộc
COPY *.csproj ./
RUN dotnet restore

# Sao chép phần còn lại và xây dựng
COPY . ./
RUN dotnet publish -c Release -o out

# Tạo hình ảnh cho ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "YourProjectName.dll"]
