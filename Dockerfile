FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /publish
COPY publish/ ./
EXPOSE 5002
ENTRYPOINT ["dotnet", "Application.dll", "--urls=http://::5002"]
