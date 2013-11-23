using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PictureLink.Data;
using PictureLink.GameLogic;

namespace PictureLinkMVC.Web.Models
{

    public class PlaySessionTransfer
    {

        public string GuessType { get; private set; }


        public string PlayType { get; private set; }


        public string PreviousGuess { get; private set; }

        internal PlaySessionTransfer(string guessType,
                                        string playType,
                                        string previousGuess)
        {
            this.GuessType = guessType;
            this.PlayType = playType;
            this.PreviousGuess = previousGuess ?? string.Empty;
        }

        internal static PlaySessionTransfer GetFromPlaySession(IPlaySession playSession)
        {
            string shortGuessType = GetShortGuessTypeValue(playSession.GuessType);
            string shortPlayType = GetShortPlayTypeValue(playSession.Type);
            string previousGuess = Maybe.From(playSession.PreviousGuess).Select(x => x.Content).Value;

            return new PlaySessionTransfer(
                shortGuessType, shortPlayType, previousGuess);
        }

        internal static string GetShortPlayTypeValue(PlayType playType)
        {
            switch (playType)
            {
                case PictureLink.GameLogic.PlayType.Link:
                    return "L";
                case PictureLink.GameLogic.PlayType.NewGame:
                    return "N";
                default:
                    return string.Empty;
            }
        }

        internal static string GetShortGuessTypeValue(GuessType guessType)
        {
            switch (guessType)
            {
                case PictureLink.Data.GuessType.Drawn:
                    return "D";
                case PictureLink.Data.GuessType.Written:
                    return "W";
                default:
                    return string.Empty;
            }
        }



    }
}