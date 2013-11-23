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
    public class GameScoreController : Controller
    {

        public ActionResult Index()
        {
            var gameScoring = new GameScoring();
            var playerId = WebSecurity.GetUserId(User.Identity.Name);
            var unmarkedChains = gameScoring.GetUnMarkedChains(playerId);

            var model = unmarkedChains.Select(c => new ChainSummary(c, playerId)).ToArray();
            ViewBag.ScoreInfo = ScoreInfo.GetDummyScore();
            return View(model);
        }

        public ActionResult ChainsView(bool toMark)
        {
            var gameScoring = new GameScoring();
            var playerId = WebSecurity.GetUserId(User.Identity.Name);
            var unmarkedChains = gameScoring.GetUnMarkedChains(playerId);

            var model = unmarkedChains.Select(c => new ChainSummary(c, playerId)).ToArray();
            ViewBag.ScoreInfo = ScoreInfo.GetDummyScore();
            ViewBag.ToMark = toMark;
            return View(model);
        }

        public ActionResult ChainView(int id, bool toMark)
        {
            var gameScoring = new GameScoring();
            var chain = gameScoring.GetChain(id);
            var guessesForMarking = chain.Guesses.Select(x => new GuessForMarking(x)).ToList();
            ViewBag.ScoreInfo = ScoreInfo.GetDummyScore();
            ViewBag.ToMark = toMark;
            return View(guessesForMarking);
        }

        [HttpPost]
        public ActionResult Submit(List<GuessForMarking> marks)
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);
            var gameScoring = new GameScoring();
            gameScoring.AwardMarks(userId, marks.Select(m => Tuple.Create(m.GuessId, m.Mark)).ToArray());
            return RedirectToAction("Index");
        }

    }
}
