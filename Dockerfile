FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Service/Service.csproj", "Service/"]
RUN dotnet restore "Service/Service.csproj"
COPY . .
WORKDIR "/src/Service"
RUN dotnet build "Service.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Service.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Service.dll"]
