using Assets.Scripts.Exceptions;
using System.Collections.Generic;

namespace Assets.Scripts.Gameplay
{
    /// <summary>
    /// It is a stateless object that returns all active players from its ActivePlayers method.
    /// </summary>
    public class ActivePlayersHandler
    {
        private List<PlayerCreed> players;
        public bool IsEnabled { get; set; } = true;

        private ActivePlayersHandler(List<PlayerCreed> selectedPlayers)
        {
            players = selectedPlayers;
        }

        public IEnumerable<PlayerCreed> ActivePlayers()
        {
            int iterator = 0;
            while (IsEnabled)
            {
                if (iterator == players.Count)
                {
                    iterator = 0;
                }
                yield return players[iterator++];
            }
        }

        public class ActivePlayersHandlerBuilder
        {
            private readonly List<PlayerCreed> enabledPlayers = new(new PlayerCreed[] { PlayerCreed.None, PlayerCreed.None, PlayerCreed.None, PlayerCreed.None });

            public ActivePlayersHandlerBuilder SetPlayer1(PlayerCreed creed) => SetCreed(0, creed);
            public ActivePlayersHandlerBuilder SetPlayer2(PlayerCreed creed) => SetCreed(1, creed);
            public ActivePlayersHandlerBuilder SetPlayer3(PlayerCreed creed) => SetCreed(2, creed);
            public ActivePlayersHandlerBuilder SetPlayer4(PlayerCreed creed) => SetCreed(3, creed);

            private ActivePlayersHandlerBuilder SetCreed(int index, PlayerCreed creed)
            {
                enabledPlayers[index] = creed;
                return this;
            }

            public ActivePlayersHandler Build()
            {
                enabledPlayers.RemoveAll(creed => creed == PlayerCreed.None);

                if (enabledPlayers.Count < 2)
                {
                    throw new NotEnoughPlayableCreedsException();
                }

                return new ActivePlayersHandler(enabledPlayers);
            }
        }
    }
}
