using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Microsoft.AspNet.SignalR;
using PictureLink.Data;
using PictureLink.GameLogic;

namespace PictureLink.Web
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PictureLinkGameService
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string UploadImage(string image, string id)
        {
            var player = new Player(id);
            var guess = new GuessInfo(player, image, GuessType.Drawn);
            GameHub.GameManager.AddGuess(guess);
            return "great";

        }

        [OperationContract]
        [WebGet]
        public PlaySessionTransfer GetPlaySession(string id)
        {
            var player = new Player(id);
            var session = GameHub.GameManager.GetPlaySession(player);
            return PlaySessionTransfer.GetFromPlaySession(session);
        }

        // Add more operations here and mark them with [OperationContract]
    }

    [DataContract]
    public class PlaySessionTransfer
    {
        [DataMember]
        public string GuessType { get; private set; }

        [DataMember]
        public string PlayType { get; private set; }

        [DataMember]
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
                case GameLogic.PlayType.Link:
                    return "L";
                case GameLogic.PlayType.NewGame:
                    return "N";
                default:
                    return string.Empty;
            }
        }

        internal static string GetShortGuessTypeValue(GuessType guessType)
        {
            switch (guessType)
            {
                case Data.GuessType.Drawn:
                    return "D";
                case Data.GuessType.Written:
                    return "W";
                default:
                    return string.Empty;
            }
        }



    }
}
