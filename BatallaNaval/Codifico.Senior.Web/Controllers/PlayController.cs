using System.Web.Mvc;
using System.Linq;
using Codifico.Senior.Core.Class;

namespace Codifico.Senior.Web.Controllers
{
    public class PlayController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Usuarios = ServerBattle.getUsers();
             
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.Player jugador, string lstUsuarios)
        {
            jugador.Host = HttpContext.Session.SessionID;
            ServerBattle.RegisterUser(jugador);
            if (string.IsNullOrWhiteSpace(lstUsuarios))
            {
                var jugador2 = ServerBattle.Gamers.FirstOrDefault(x => x.Player1.Name == jugador.Name || x.Player2.Name == jugador.Name);
                if (jugador2 != null)
                {
                    var seesionId = (jugador2.Player1.Name == jugador.Name) ? jugador2.Player1.Host : jugador2.Player2.Host;
                    return RedirectToAction("Index", "Game", new { sessionId = seesionId });
                }
                else
                    return View();
            }
            else
            {
                var game = new Game { Player1 = jugador, Player2 = ServerBattle.User.Find(x => x.Name == lstUsuarios) };
                ServerBattle.RegisterGame(game);

                return RedirectToAction("Index", "Game", new { sessionId = jugador.Host });
            }
        }
    }
}