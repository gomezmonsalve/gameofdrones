using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Games.Drones.Models;

namespace Games.Drones.Controllers
{
    public class GamesController : ApiController
    {
        private DronesContext db = new DronesContext();

        // GET: api/Games
        public IQueryable<Game> GetGames()
        {
            return db.Games;
        }

        // GET: api/Games/5
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> GetGame(int id)
        {
            Game game = await db.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        [ResponseType(typeof(List<Statistic>))]
        [Route("api/Games/Statistics")]
        [HttpGet]
        public async Task<IHttpActionResult> GetGameStatistic()
        {
            var listWinnersData = await db.Games.GroupBy(gm=>gm.Winner).ToArrayAsync();
            List<Statistic> listWinners = new List<Statistic>();
            foreach (var winnerData in listWinnersData)
            {
                Statistic data = new Statistic();
                if (!string.IsNullOrEmpty(winnerData.Key) && winnerData.Key!="None")
                {
                    data.Name = winnerData.Key;
                    data.Count = winnerData.Count();
                    listWinners.Add(data);
                }                
                
            }
            if (listWinners == null)
            {
                return NotFound();
            }
            listWinners = listWinners.OrderByDescending(w => w.Count).ToList();
            return Ok(listWinners);
        }

        // PUT: api/Games/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGame(int id, Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != game.Id)
            {
                return BadRequest();
            }

            db.Entry(game).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Games
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> PostGame(Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            game.CreatedDate = DateTime.Now;
            db.Games.Add(game);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> DeleteGame(int id)
        {
            Game game = await db.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            db.Games.Remove(game);
            await db.SaveChangesAsync();

            return Ok(game);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameExists(int id)
        {
            return db.Games.Count(e => e.Id == id) > 0;
        }
    }
}