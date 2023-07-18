using Assets.Scripts.Gameplay;
using System;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts.Utils
{
    public class PlayerUIHandler : IDisposable
    {
        private readonly Player player;
        private readonly TextMeshProUGUI textName;
        private readonly TextMeshProUGUI textPoints;

        public PlayerUIHandler(Player player, TextMeshProUGUI textName, TextMeshProUGUI textPoints)
        {
            this.player = player;
            this.textName = textName;
            this.textPoints = textPoints;

            Setup();
        }

        private void Setup()
        {
            if (player != null && player?.Creed != PlayerCreed.None)
            {
                textName.text = player.Name;
                textPoints.text = player.AchievedPoints.ToString();
                player.OnPointAchieved += Player_OnPointAchieved;
            }
            else
            {
                textName.text = "";
                textPoints.text = "";
                var image = textName.GetComponentInParent<Image>();
                image.enabled = false;
            }
        }

        private void Player_OnPointAchieved()
        {
            textPoints.text = player.AchievedPoints.ToString();
        }

        public void Dispose()
        {
            if (player.Creed != PlayerCreed.None) player.OnPointAchieved -= Player_OnPointAchieved;
        }
    }
}
