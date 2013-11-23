using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PictureLink.Data;

namespace PictureLinkMVC.Web.Models
{
    public interface IChainSummary
    {
        PlayerGuess PlayerGuess { get; set; }
        string[] OtherContributors { get; set; }
        int ChainId { get; }
    }

    public class ChainSummary : IChainSummary
    {
        public PlayerGuess PlayerGuess { get; set; }
        public string[] OtherContributors { get; set; }
        public int ChainId { get; set; }
        internal ChainSummary(Chain chain, int playerId)
        {
            this.ChainId = chain.Id;
            var playerGuess = chain.Guesses.FirstOrDefault(g => g.Contributor.UserId == playerId);
            this.PlayerGuess = new PlayerGuess(playerGuess.Content, playerGuess.Type.ToString());
            this.OtherContributors =
                chain.Guesses.Where(g => g.Contributor.UserId != playerId).Select(g => g.Contributor.UserName).ToArray();
        }

    }

    public class GuessForMarking 
    {
        public int Mark
        {
            get; set; }

        public string ContributorName { get; set; }

        public string Content { get; set; }

        public string ContentType { get; set; }

        public SelectList AvailableMarks
        {
            get; set; }

        public int GuessId
        {
            get; set; }

        public GuessForMarking(Guess guess)
        {
            this.ContributorName = guess.Contributor.UserName;
            this.Content = guess.Content;
            this.ContentType = guess.Type;
            this.GuessId = GuessId;
            this.AvailableMarks = new SelectList(new[] { 0, 1, 2, 3 });

        }

        public GuessForMarking()
        {
        }

    }



    public class PlayerGuess
    {
        public string Content { get; set; }
        public string Type { get; set; }

        public PlayerGuess(string content, string type)
        {
            this.Content = content;
            this.Type = type;
        }
    }

    public class ScoreInfo
    {
        public int Score { get; set; }
        public Tuple<string, int>[] Leaders { get; set; }
        public static ScoreInfo GetDummyScore()
        {
            var si = new ScoreInfo {Score = 12, Leaders = new[] {Tuple.Create("John", 45), Tuple.Create("Bloke", 3)}};
            return si;
        }
    }
}