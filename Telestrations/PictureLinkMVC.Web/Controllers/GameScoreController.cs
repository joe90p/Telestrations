using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PictureLink.GameLogic;
using PictureLinkMVC.Web.Models;

namespace PictureLinkMVC.Web.Controllers
{
    public class GameScoreController : Controller
    {
        //
        // GET: /GameScore/

        public ActionResult Index()
        {
            var gameScoring = new GameScoring();
            var playerId = 2;
            var unmarkedChains = gameScoring.GetUnMarkedChains(playerId);

            var model = unmarkedChains.Select(c => new ChainSummary(c, playerId)).ToArray();

            return View(model);
        }

    }
}
