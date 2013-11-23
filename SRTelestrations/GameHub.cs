using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using PictureLink.Data;
using PictureLink.GameLogic;

namespace PictureLink.Web
{
    public class GameHub : Hub
    {
        public static Game GameManager = new Game();

        public void Send(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);
        }

        public void Register()
        {

        }

        /*public void AddWriitenGuess(string guess)
        {
            var playerId = Context.ConnectionId;
            var player = new Player(playerId);
            var guessObj = new GuessInfo(player,guess, GuessType.Written);
            GameManager.AddGuess(guessObj);
        }*/

        public override Task OnDisconnected()
        {
            //var playerId = Context.ConnectionId;
            //var player = new Player(playerId);
            //GameManager.RemovePlayer(player);
            return base.OnDisconnected();
        }

    }




}