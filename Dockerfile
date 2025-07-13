# استخدم صورة .NET SDK للبناء
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# نسخ ملفات المشروع
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# استخدم صورة runtime لتشغيل التطبيق
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# تشغيل التطبيق
ENTRYPOINT ["dotnet", "PersonalPortfolio.dll"]
