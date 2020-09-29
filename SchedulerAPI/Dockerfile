FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
ADD https://github.com/ufoscout/docker-compose-wait/releases/download/2.7.3/wait /wait
RUN chmod +x /wait
CMD /wait
COPY ["SchedulerAPI/SchedulerAPI.csproj", "SchedulerAPI/"]
RUN dotnet restore "SchedulerAPI/SchedulerAPI.csproj"
COPY . .
WORKDIR "/src/SchedulerAPI"
RUN dotnet build "SchedulerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SchedulerAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SchedulerAPI.dll"]