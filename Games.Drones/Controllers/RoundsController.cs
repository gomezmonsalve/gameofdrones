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
using Games.Drones.BusinessRules;

namespace Games.Drones.Controllers
{
    public class RoundsController : ApiController
    {
        private DronesContext db = new DronesContext();

        // GET: api/Rounds
        public IQueryable<Round> GetRounds()
        {
            return db.Rounds;
        }

        // GET: api/Rounds/5
        [ResponseType(typeof(Round))]
        public async Task<IHttpActionResult> GetRound(int id)
        {
            Round round = await db.Rounds.FindAsync(id);
            if (round == null)
            {
                return NotFound();
            }

            return Ok(round);
        }


        // GET: api/Rounds/5
        [ResponseType(typeof(Round []))]
        [Route("api/Rounds/Game/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetRoundsByGameId(int id)
        {
            Round [] rounds = await db.Rounds.Where(rd => rd.GameId == id).ToArrayAsync();
            if (rounds == null)
            {
                return NotFound();
            }

            return Ok(rounds);
        }

        // PUT: api/Rounds/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRound(int id, Round round)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != round.Id)
            {
                return BadRequest();
            }

            db.Entry(round).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoundExists(id))
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

       
        // POST: api/Rounds
        /// <summary>
        /// Post Round information
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        [ResponseType(typeof(Round))]
        public async Task<IHttpActionResult> PostRound(Round round)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            round.CreatedDate = DateTime.Now;
            round.WinnerPlayer = DronesLogic.SetRoundWinner(round);
            db.Rounds.Add(round);
            await db.SaveChangesAsync();
            int winner = DronesLogic.UpdateGameWinner(db.Rounds.Where(rd=>rd.GameId==round.GameId).ToList());
            if(winner>0)
            {
                Game currenGame = db.Games.Find(round.GameId);
                if(winner==1)
                {
                    currenGame.Winner = currenGame.Player1;
                }
                else if(winner==2)
                {
                    currenGame.Winner = currenGame.Player2;
                }
                db.Entry(currenGame).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

            return CreatedAtRoute("DefaultApi", new { id = round.Id }, round);
        }

        // DELETE: api/Rounds/5
        [ResponseType(typeof(Round))]
        public async Task<IHttpActionResult> DeleteRound(int id)
        {
            Round round = await db.Rounds.FindAsync(id);
            if (round == null)
            {
                return NotFound();
            }

            db.Rounds.Remove(round);
            await db.SaveChangesAsync();

            return Ok(round);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoundExists(int id)
        {
            return db.Rounds.Count(e => e.Id == id) > 0;
        }
    }
}