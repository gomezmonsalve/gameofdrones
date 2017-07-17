using System;
using System.Linq;
using System.Threading.Tasks;
using Games.Drones.Models;
using Games.Drones.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Games.Drones.Tests
{
    [TestClass]
    public class GameTest
    {
        private DronesContext db = new DronesContext();
        [TestMethod]
        public async Task PostGame()
        {          
            Game game = new Game
            {
               Player1= "nameOne",
               Player2="nameTwo",
               Winner ="none"
            };
            var intIdBeforeAdd = db.Games.Max(gm => gm.Id);
            var controller = new GamesController();
            var response = await controller.PostGame(game);
            Assert.IsNotNull(response);
            Game lastItem = db.Games.ToList().Last();
            var sameItem = CheckItemProperties(game, lastItem);
            Assert.AreEqual(intIdBeforeAdd + 1, lastItem.Id);
            Assert.IsTrue(sameItem);

        }

        private bool CheckItemProperties(Game sentValue, Game returnValue)
        {
            if (sentValue.Player1 == returnValue.Player1 &&
                sentValue.Player2 == returnValue.Player2 &&
                 sentValue.Winner == returnValue.Winner)
            {
                return true;
            }

            return false;
        }
    }
}
