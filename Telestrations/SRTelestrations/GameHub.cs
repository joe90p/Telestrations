using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
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
            var playerId = Context.ConnectionId;
            var player = new Player(playerId);
            if (GameManager.IsPlayerInGame(player))
            {
                Clients.Caller.broadcastMessage("GameHub", "You are already registered.");
            }
            else
            {
                GameManager.AddPlayer(player);
                Clients.Caller.broadcastMessage("GameHub", "You successfully registered.");
            }
        }

        public void AddWriitenGuess(string guess)
        {
            var playerId = Context.ConnectionId;
            var player = new Player(playerId);
            var guessObj = new GuessInfo(player,guess, GuessType.Written);
            GameManager.AddGuess(guessObj);
        }

        public override Task OnDisconnected()
        {
            //var playerId = Context.ConnectionId;
            //var player = new Player(playerId);
            //GameManager.RemovePlayer(player);
            return base.OnDisconnected();
        }

    }




}