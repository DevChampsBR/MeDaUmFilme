FROM microsoft/dotnet:1.0.1-sdk-projectjson
ENTRYPOINT ["dotnet", "MeDaUmFilme.dll"]
ARG source=.
WORKDIR /app
COPY $source .