using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PictureLink.Data;

namespace PictureLinkMVC.Web.Models
{

    public interface IChainSummary
    {
        PlayerGuess PlayerGuess { get; set; }
        string[] OtherContributors { get; set; }
    }

    public class ChainSummary : IChainSummary
    {
        public PlayerGuess PlayerGuess { get; set; }
        public string[] OtherContributors { get; set; }
        internal ChainSummary(IChainDTO chain, int playerId)
        {
            var playerGuess = chain.Guesses.FirstOrDefault(g => g.Contributor.Id == playerId.ToString());
            this.PlayerGuess = new PlayerGuess(playerGuess.Content, playerGuess.Type.ToString());
            this.OtherContributors =
                chain.Guesses.Where(g => g.Contributor.Id != playerId.ToString()).Select(g => g.Contributor.Name).ToArray();
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
}