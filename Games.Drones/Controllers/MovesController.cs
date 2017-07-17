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
    public class MovesController : ApiController
    {
        private DronesContext db = new DronesContext();

        // GET: api/Moves
        public IQueryable<Move> GetMoves()
        {
            return db.Moves;
        }

        // GET: api/Moves/5
        [ResponseType(typeof(Move))]
        public async Task<IHttpActionResult> GetMove(int id)
        {
            Move move = await db.Moves.FindAsync(id);
            if (move == null)
            {
                return NotFound();
            }

            return Ok(move);
        }

        // PUT: api/Moves/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMove(int id, Move move)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != move.Id)
            {
                return BadRequest();
            }

            db.Entry(move).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoveExists(id))
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

        // POST: api/Moves
        [ResponseType(typeof(Move))]
        public async Task<IHttpActionResult> PostMove(Move move)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Moves.Add(move);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = move.Id }, move);
        }

        // DELETE: api/Moves/5
        [ResponseType(typeof(Move))]
        public async Task<IHttpActionResult> DeleteMove(int id)
        {
            Move move = await db.Moves.FindAsync(id);
            if (move == null)
            {
                return NotFound();
            }

            db.Moves.Remove(move);
            await db.SaveChangesAsync();

            return Ok(move);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MoveExists(int id)
        {
            return db.Moves.Count(e => e.Id == id) > 0;
        }
    }
}