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
        private readonly Dictionary<PlayerCreed, Player> _activePlayers = new();

        public bool IsEnabled { get; set; } = true;
        public int PlayerCount => _activePlayers.Count;

        private ActivePlayersHandler(List<Player> selectedPlayers)
        {
            foreach (Player player in selectedPlayers)
            {
                _activePlayers.Add(player.Creed, player);
            }
        }

        public Player GetPlayerByCreed(PlayerCreed creed)
        {
            return _activePlayers.TryGetValue(creed, out Player player)
                ? player
                : throw new InvalidCreedException($"Can't find player that is {creed.DisplayName()}!");
        }

        public IEnumerable<Player> GetNextPlayer()
        {
            int iterator = 0;
            while (IsEnabled)
            {
                if (iterator == _activePlayers.Count)
                {
                    iterator = 0;
                }

                yield return _activePlayers[CreedSelector.GetCreedBasedOnIndex(iterator)];
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
                _ = addedPlayers.RemoveAll(player => player == null);

                return addedPlayers.Count < 2
                    ? throw new NotEnoughPlayersException()
                    : addedPlayers.Exists(player => string.IsNullOrWhiteSpace(player.Name))
                    ? throw new InvalidPlayerException($"All players need to have names assigned to them!")
                    : new ActivePlayersHandler(addedPlayers);
            }
        }
    }
}
