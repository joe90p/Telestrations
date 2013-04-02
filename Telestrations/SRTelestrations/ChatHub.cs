using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SRTelestrations
{


    public class ChatHub : Hub
    {
        public static GameManager GameManager = new GameManager();


        public void Send(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);
        }

        public void Register()
        {
            string playerId = Context.ConnectionId;
            if (GameManager.IsPLayerRegisteredWithGame(playerId))
            {
                Clients.Caller.broadcastMessage("Server", "You are already registered.");
            }
            else
            {
                string gamename = GameManager.AddPlayerToAvailableGame(playerId);
                Clients.Caller.broadcastMessage("Server", "You registered with game " + gamename);
            }
        }


    }


    public class GameManager
    {
        public List<Game> games = new List<Game>();
        public static int gameId = 0;
        

        public Game GetGame(string name)
        {
            return this.games.FirstOrDefault(g => g.Name == name);
        }
        public bool IsPLayerRegisteredWithGame(string playerId)
        {
            return this.games.Any(g => g.IsPlayerRegistered(playerId));
        }

        public string AddPlayerToAvailableGame(string playerId)
        {
            var availableGame = this.games.FirstOrDefault(g => g.IsAvailable(playerId));
            if(availableGame == null)
            {
                gameId++;
                availableGame = new Game(gameId.ToString());
                this.games.Add(availableGame);
            }
            availableGame.Register(playerId);
            return availableGame.Name;

        }

        public void AddItemForPlayer(string item, string playerId)
        {
            var game = this.games.FirstOrDefault(g=> g.IsPlayerRegistered(playerId));
            if(game != null && game.State == GameState.InPlay)
            {
                game.AddPlayerItem(playerId, item);
            }
            else
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                context.Clients.Client(playerId).broadcastMessage("Server", "You are either not registered or the game is not in play.");
            }
        }
 
    }

    public class Game
    {
        public Game(string id)
        {
            this.Name = id;
        }
        public GameState State = GameState.Initializing;

        private static int numberOfPlayers = 4;
        public List<string> Players = new List<string>();
        public Dictionary<string, List<string>> PlayerItems = new Dictionary<string, List<string>>(); 
        public string Name { get; set; }
        public int Round = 0;

        public bool IsPlayerRegistered(string playerId)
        {
            return Players.Contains(playerId);
        }

        public bool IsAvailable(string playerId)
        {
            return this.State!=GameState.InPlay && !this.Players.Contains(playerId);
        }


        public void Register(string playerId)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            
            var addGroupTask = context.Groups.Add(playerId, this.Name);

            addGroupTask.ContinueWith(
                t =>
                    {
                        this.Players.Add(playerId);
                        this.PlayerItems.Add(playerId, new List<string>());
                        if(this.Players.Count == numberOfPlayers)
                        {
                            context.Clients.Group(this.Name)
                               .broadcastMessage(
                                   "Server",
                                   String.Format(
                                       "The required number of players has been met. Please submit.",
                                       numberOfPlayers - this.Players.Count));
                            this.State = GameState.InPlay;
                            Round++;
                            this.DistributeGuesses(context);
                        }
                        else
                        {
                            context.Clients.Group(this.Name)
                               .broadcastMessage(
                                   "Server",
                                   String.Format(
                                       "Another player registered. Waiting for {0} more",
                                       numberOfPlayers - this.Players.Count));
                        }
                        
                        
                    });
                

        }

        public void AddPlayerItem(string player, string item)
        {
            var items = PlayerItems[player];
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            if(items.Count == Round)
            {
                context.Clients.Client(player)
                       .broadcastMessage("Server", "You have submitted your entry for this round");
            }
            else
            {
                items.Add(item);
                var count = this.PlayerItems.Count(k => k.Value.Count < this.Round);
                if(count == 0)
                {
                    Round++;
                    context.Clients.All.broadcastMessage("Server", String.Format("All entries submitted for this round. Moving onto round {0}", Round));
                    //context.Clients.All.roundChange(Round%2==0, )
                }
                else
                {
                    context.Clients.All.broadcastMessage("Server", String.Format("Entry submitted. Waiting on {0} players.", count));
                }
                
                
            }
            
        }

        public void DistributeGuesses(IHubContext context)
        {
            int counter = 0;
            foreach(var player in PlayerItems)
            {
                var adjacentPlayerIndex = (counter + (numberOfPlayers - 1)) % numberOfPlayers;
                var adjacentPlayer = PlayerItems.ElementAt(adjacentPlayerIndex);
                //var player = PlayerItems.ElementAt(counter);
                context.Clients.Client(player.Key).roundChange(adjacentPlayer.Key, Round % 2 == 0);
                counter++;
            }
            
            
        }


    }

    public enum GameState
    {
        Initializing,
        InPlay
    }




}