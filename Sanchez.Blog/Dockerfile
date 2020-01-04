FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Sanchez.Blog/Sanchez.Blog.csproj", "Sanchez.Blog/"]
COPY ["Sanchez.Blog.Services/Sanchez.Blog.Services.fsproj", "Sanchez.Blog.Services/"]
COPY ["Sanchez.Blog.Core/Sanchez.Blog.Core.fsproj", "Sanchez.Blog.Core/"]
RUN dotnet restore "Sanchez.Blog/Sanchez.Blog.csproj"
COPY . .
WORKDIR "/src/Sanchez.Blog"
RUN dotnet build "Sanchez.Blog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sanchez.Blog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sanchez.Blog.dll"]
