
using WebAPI_Grundlagen.Models.DB;

/*
 *  1. Klassenbibliothek erstellen
 *      a. Klasse Article vom WebAPI-Projekt in diese Bibl. verschieben
 *          (WICHTIG: Klasse Article darf nicht mehr in der WebAPI enthalten sein)
 *          - Namespace-Namen anpassen
 *          
 *      b. Bibliothek ins WebAPI-Projekt einbinden
 *      c. testen, ob alles funktioniert (WebAPI-Projekt)
 *  
 *  2. KonsolenApp erzeugen (Client für den Zugriff auf die WebAPI)
 *      a. Klasse HttpClient verwenden (besitzt methoden für
 *          POST-, GET-, ... Anfragen)
 *      b. alle Methoden der WebAPI von dieser KonsolenApp aufrufen und
 *              Ergebnisse ausgeben
 *          (alle Artikel abfragen und anzeigen, ein Artikel anzeigen, einen
 *              Artikel löschen, einen neuen Artikel in der DB anlegen,
 *              einen vorh. Artikel ändern)
 */


namespace WebAPI_Grundlagen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // wir geben dem DI-Container die DB-Instanz bekannt
            //      er erzeugt für uns bei Bedarf diese Instanz
            builder.Services.AddDbContext<DbManager>(ServiceLifetime.Singleton);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
