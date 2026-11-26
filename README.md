# JStudio API – annonser för pennor

Ett REST-API för annonser om pennor (ungefär som en köp- och säljsajt). Med API:t kan en klient skapa, läsa, ändra och ta bort annonser. Data skickas som JSON via HTTP mellan klienten (till exempel Swagger eller `.http`-filen) och servern.

Inlämningsuppgift **APIAnnons** i kursen *Programmering med C# .NET*, YH Akademin.

## Teknik

- C# och **ASP.NET Core Web API** med controllers, .NET 10
- **Entity Framework Core** (Code First) mot **SQL Server**
- **Swagger UI** (OpenAPI) för att se och testa endpoints
- Lager: Controller → Service (med interface, via Dependency Injection) → DbContext → databas

## Det här behöver du

- **.NET 10 SDK** (verktygen för att bygga och köra .NET-projekt, ingår oftast i Visual Studio)
- **Visual Studio** med arbetsbelastningen *ASP.NET and web development*
- **SQL Server** på den egna datorn (`localhost`, Windows Authentication)

NuGet-paketen (EF Core, Swashbuckle med flera) står i `JStudio.Api.csproj` och hämtas automatiskt när projektet byggs.

## Kom igång

1. Klona repot och öppna `JStudio.slnx` i Visual Studio.
2. Kontrollera anslutningssträngen i `JStudio.Api/appsettings.json`:
   ```
   Server=localhost;Database=JStudioDb;Trusted_Connection=True;TrustServerCertificate=true
   ```
   Har din SQL Server ett annat namn, byter du ut `localhost` efter `Server=` mot det namnet. Namnet ser du i SSMS när du ansluter (rutan *Server name*). Till exempel: `Server=localhost\SQLEXPRESS;Database=JStudioDb;...`
3. Tryck **F5**.
   - Databasen **`JStudioDb` skapas automatiskt** vid start (`Database.Migrate()` i `Program.cs`). Du behöver inte köra `Update-Database`.
   - **Swagger** öppnas direkt på startsidan, `https://localhost:7170/`.
4. Databasen är **tom** från början, eftersom ingen seed-data finns. Skapa en annons med `POST /Ads` först.

## Endpoints

| Endpoint | Gör | Lyckas | Hittas inte | Ogiltig data |
|---|---|---|---|---|
| `GET /Ads` | Hämtar alla annonser | 200 OK | – | – |
| `GET /Ads/{id}` | Hämtar en annons | 200 OK | 404 Not Found | 400 Bad Request |
| `POST /Ads` | Skapar en annons | 201 Created (med `Location`-header) | – | 400 Bad Request |
| `PUT /Ads/{id}` | Ersätter alla fält i en annons | 204 No Content | 404 Not Found | 400 Bad Request |
| `DELETE /Ads/{id}` | Tar bort en annons | 204 No Content | 404 Not Found | 400 Bad Request |

Exempel på body för `POST` och `PUT`:

```json
{
  "title": "BallpointPens 0,4 mm, mörkgrå",
  "price": 54,
  "description": "Knappt använd, fungerar som ny!",
  "location": "Strängnäs",
  "imageUrl": null,
  "category": "BallpointPens"
}
```

`category` skrivs som text. Tillåtna värden: `FinelinerPens`, `MechanicalPencils`, `WatercolorPencils`, `ErasablePens`, `FeltTipPens`, `BallpointPens`, `GelPens`, `Other`.

`id`, `created` och `updated` sätts av servern, inte av klienten.

## Testa

**Swagger:** välj en endpoint → *Try it out* → *Execute*.

**`.http`-filen:** öppna `JStudio.Api/JStudio.Api.http` i Visual Studio och klicka på *Send request* ovanför ett anrop. Filen innehåller 13 tester (normalfall, id som inte finns, felaktig data och fel metod), och varje test har en kommentar med förväntad statuskod.

- Kör `POST` först och byt `28` i PUT- och DELETE-blocken mot det id som POST ger.
- API:t måste vara igång (F5). Står det *"An error occurred while sending the request"* är API:t inte startat.
