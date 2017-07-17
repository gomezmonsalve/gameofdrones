using Games.Drones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Games.Drones.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            //Initial Data
            DronesContext db = new DronesContext();

            Move moveRock = db.Moves.FirstOrDefault(m => m.Name == "Rock");
            if(moveRock==null)
            {
                Move move = new Move();
                move.Name = "Rock";
                db.Moves.Add(move);
            }

            Move movePapper = db.Moves.FirstOrDefault(m => m.Name == "Papper");
            if (movePapper == null)
            {
                Move move = new Move();
                move.Name = "Papper";
                db.Moves.Add(move);
            }

            Move moveScissors = db.Moves.FirstOrDefault(m => m.Name == "Scissors");
            if (moveScissors == null)
            {
                Move move = new Move();
                move.Name = "Scissors";
                db.Moves.Add(move);
            }  

            db.SaveChanges();

            return View();
        }
    }
}
