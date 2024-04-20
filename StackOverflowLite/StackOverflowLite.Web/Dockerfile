FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y nodejs
COPY ["StackOverflowLite.Web/*.csproj", "StackOverflowLite.Web/"]
COPY ["StackOverflowLite.Application/*.csproj", "StackOverflowLite.Application/"]
COPY ["StackOverflowLite.Domain/*.csproj", "StackOverflowLite.Domain/"]
COPY ["StackOverflowLite.Infrastructure/*.csproj", "StackOverflowLite.Infrastructure/"]
RUN dotnet restore "StackOverflowLite.Web/StackOverflowLite.Web.csproj"
COPY . .
WORKDIR "/src/StackOverflowLite.Web"
RUN dotnet build "StackOverflowLite.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "StackOverflowLite.Web.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "Exam.Web.dll"]