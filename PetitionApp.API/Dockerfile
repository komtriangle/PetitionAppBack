#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PetitionApp.API/PetitionApp.API.csproj", "PetitionApp.API/"]
COPY ["PetitionApp.Data/PetitionApp.Data.csproj", "PetitionApp.Data/"]
COPY ["PetitionApp.Core/PetitionApp.Core.csproj", "PetitionApp.Core/"]
RUN dotnet restore "PetitionApp.API/PetitionApp.API.csproj"
COPY . .
WORKDIR "/src/PetitionApp.API"
RUN dotnet build "PetitionApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PetitionApp.API.csproj" -c Release -o /app/publish




FROM base AS final


WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetitionApp.API.dll"]