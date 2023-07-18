using Assets.Scripts.Exceptions;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Assets.Scripts.Gameplay
{
    /// <summary>
    /// It is a stateless object that returns all active players from its ActivePlayers method.
    /// </summary>
    public class ActivePlayersHandler
    {
        private readonly Dictionary<PlayerCreed, Player> activePlayers = new();

        public bool IsEnabled { get; set; } = true;
        public int PlayerCount => activePlayers.Count;

        private ActivePlayersHandler(List<Player> selectedPlayers)
        {
            foreach (var player in selectedPlayers)
            {
                activePlayers.Add(player.Creed, player);
            }
        }

        public Player GetPlayerByCreed(PlayerCreed creed)
        {
            if (activePlayers.TryGetValue(creed, out var player))
            {
                return player;
            }
            else
            {
                throw new InvalidCreedException($"Can't find player that is {creed.DisplayName()}!");
            }
        }

        public IEnumerable<Player> GetNextPlayer()
        {
            int iterator = 0;
            while (IsEnabled)
            {
                if (iterator == activePlayers.Count) iterator = 0;
                yield return activePlayers[CreedSelector.GetCreedBasedOnIndex(iterator)];
            }
        }

        public class ActivePlayersHandlerBuilder
        {
            private readonly List<Player> addedPlayers = new(4) { null, null, null, null };

            public ActivePlayersHandlerBuilder SetPlayer(Player player)
            {
                int playerIndex = CreedSelector.GetIndexBasedOnCreed(player.Creed);

                if (playerIndex != -1)
                {
                    if (addedPlayers[playerIndex] != null)
                    {
                        throw new InvalidPlayerException($"Player with creed {player.Creed} already set!");
                    }

                    addedPlayers.RemoveAt(playerIndex);
                    addedPlayers.Insert(playerIndex, player);
                }
                return this;
            }

            public ActivePlayersHandler Build()
            {
                addedPlayers.RemoveAll(player => player == null);

                if (addedPlayers.Count < 2)
                {
                    throw new NotEnoughPlayersException();
                }
                if (addedPlayers.Exists(player => string.IsNullOrWhiteSpace(player.Name)))
                {
                    throw new InvalidPlayerException($"All players need to have names assigned to them!");
                }

                return new ActivePlayersHandler(addedPlayers);
            }
        }
    }
}
