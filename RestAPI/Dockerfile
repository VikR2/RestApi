FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["RestAPI/RestAPI.csproj", "RestAPI/"]
RUN dotnet restore "RestAPI/RestAPI.csproj"
COPY . .
WORKDIR "/src/RestAPI"
RUN dotnet build "RestAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RestAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RestAPI.dll"]