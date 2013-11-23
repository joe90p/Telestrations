using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PictureLink.Data;
using PictureLink.GameLogic;
using PictureLinkMVC.Web.Models;
using WebMatrix.WebData;

namespace PictureLinkMVC.Web.Controllers
{
    [Authorize]
    public class PlayController : Controller
    {
        //
        // GET: /Play/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Play()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);
            var player = new Player(userId);
            var playSession = Game.Instance.GetPlaySession(player);
            var model = PlaySessionTransfer.GetFromPlaySession(playSession);

            return View(model);
        }

        public ActionResult SubmitGuess(FormCollection coll)
        {
            var guessValue = coll["guessValue"];
            var guessType = coll["guessType"];
            var userId = WebSecurity.GetUserId(User.Identity.Name);
            var player = new Player(userId);
            var guessInfo = new GuessInfo(player, guessValue, guessType=="W" ? GuessType.Written : GuessType.Drawn);
            Game.Instance.AddGuess(guessInfo);
            return new EmptyResult();
        }

    }
}
