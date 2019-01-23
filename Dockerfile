FROM microsoft/dotnet:sdk AS build
WORKDIR /app

#copy csproj and restore as distint layers

COPY CarAdverts/*.csproj ./CarAdverts/
WORKDIR /app/CarAdverts
RUN dotnet restore

#copy everything else and build app
COPY CarAdverts/. ./CarAdverts/
WORKDIR /app/CarAdverts
RUN dotnet publish -c Release -o out


# test application
FROM build AS testrunner
WORKDIR /app/CarAdverts.Tests
COPY CarAdverts.Tests/. .
ENTRYPOINT ["dotnet", "test", "--logger:trx"]

FROM microsoft/dotnet:aspnetcore-runtime as runtime
WORKDIR /app
COPY --from=build /app/CarAdverts/out ./
ENTRYPOINT ["dotnet", "CarAdverts.dll"]
