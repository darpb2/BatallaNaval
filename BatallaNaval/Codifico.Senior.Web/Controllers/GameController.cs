using Codifico.Senior.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Codifico.Senior.Core.Class;

namespace Codifico.Senior.Web.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index(string sessionId)
        {
            var game = ServerBattle.Gamers.First(x => x.Player1.Host == sessionId || x.Player2.Host == sessionId);
            var gamer = (game.Player1.Host == sessionId) ? game.Player1 : game.Player2;

            return View(gamer);
        }

        [HttpPost]
        public ActionResult Jugar(string id)
        {
            var arreglo = id.Split('-');
            var sessionId = arreglo[0];
            Point point = new Point { X = int.Parse(arreglo[1]), Y = int.Parse(arreglo[2]) };
            var game = ServerBattle.Gamers.First(x => x.Player1.Host == sessionId || x.Player2.Host == sessionId);
            var gamer = (game.Player1.Host == sessionId) ? game.Player1 : game.Player2;
            var gamer2 = (game.Player1.Host != sessionId) ? game.Player1 : game.Player2;
            ServerBattle.Attack(point, gamer.Name, gamer2.Name);

            return View(gamer);
        }
    }
}