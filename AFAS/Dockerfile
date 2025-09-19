FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
COPY / /app
ENV TZ=Asia/Shanghai
ENV ASPNETCORE_URLS=http://+:5165
ENV LANG C.UTF-8
ENV NUGET_XMLDOC_MODE none
WORKDIR /app
EXPOSE 5165

ENTRYPOINT ["dotnet", "AFAS.dll"]
