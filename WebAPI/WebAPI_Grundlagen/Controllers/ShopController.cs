using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Grundlagen.Models.DB;
using WebAPI_Grundlagen.Models;
using ArticleLibrary;

/*
   Vorbereitung

        1. Klasse Article programmieren
               (ArticleId, name, Preis)
        2. ORM-Packages installieren (EF Core, Ef Core Tools, Pomelo)
        3. DbManager-Klasse programmieren
        4. Migrations erzeugen + in der DB überprüfen
            + 3 Datensätze direkt eintragen
        5. DB-Manager konfig. (ID)

    WebAPI-Teil

        1. alle Article als JSON zurückliefern
        2. einen Artikel (per Id) zurückliefern
        3. einen Artikel (per Id) löschen
        4. einen neuen Artikel erzeugen
        5. von einem vorhandenen Artikel best. Daten ändern
 */

namespace WebAPI_Grundlagen.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly DbManager _dbManager;

        // hier erkennt der DI-Container, dass eine Instanz von DbManager
        //      benötigt wird - wir haben den DbManager dem DI-Container in
        //      Program.cs bekanntgegeben. Der DI-Container übergibt an
        //      den ctor die von ihm erzeugte Dbmanager-Instanz
        public ShopController(DbManager dbManager)
        {
            this._dbManager = dbManager;
        }

        [HttpGet("articles")]
        public async Task<IActionResult> GetAllArticles()
        {
            // alle Artikel aus der DB holen
            // und nach JSON umwandeln
            return new JsonResult(await this._dbManager.Articles.ToListAsync());
        }

        [HttpGet("articles/{id:int}")]
        public async Task<IActionResult> GetArticleById(int id)
        {
            return new JsonResult(await this._dbManager.Articles.FindAsync(id));
        }

        [HttpDelete("articles/{id:int}")]
        public async Task<IActionResult> DeleteArticleById(int id)
        {
            var article = await this._dbManager.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            this._dbManager.Articles.Remove(article);
            await this._dbManager.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("articles")]
        public async Task<IActionResult> CreateArticle([FromBody] Article article)
        {
            await this._dbManager.Articles.AddAsync(article);
            await this._dbManager.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("articles/{id:int}")]
        public async Task<IActionResult> UpdateArticle(int id, [FromBody] Article article)
        {
            var dbArticle = await this._dbManager.Articles.FindAsync(id);
            if (dbArticle == null)
            {
                return NotFound();
            }
            dbArticle.Name = article.Name;
            dbArticle.Preis = article.Preis;
            await this._dbManager.SaveChangesAsync();
            return Ok();
        }
    }
}
