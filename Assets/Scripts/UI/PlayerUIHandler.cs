using Assets.Scripts.Gameplay;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utils
{
    public class PlayerUIHandler : IDisposable
    {
        private readonly Player _player;
        private readonly TextMeshProUGUI _textName;
        private readonly TextMeshProUGUI _textPoints;

        public PlayerUIHandler(Player player, CanvasRenderer playerCanvasRenderer)
        {
            _player = player;

            foreach (TextMeshProUGUI textMesh in playerCanvasRenderer.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (textMesh.CompareTag("UIPlayerNameTag"))
                {
                    _textName = textMesh;
                }
                else if (textMesh.CompareTag("UIPlayerPointsTag"))
                {
                    _textPoints = textMesh;
                }
            }

            Setup();
        }

        private void Setup()
        {
            if (_player != null && _player?.Creed != PlayerCreed.None)
            {
                _textName.text = _player.Name;
                _textPoints.text = _player.AchievedPoints.ToString();
                _player.OnPointAchieved += Player_OnPointAchieved;
            }
            else
            {
                _textName.text = "";
                _textPoints.text = "";
                Image image = _textName.GetComponentInParent<Image>();
                image.enabled = false;
            }
        }

        private void Player_OnPointAchieved()
        {
            _textPoints.text = _player.AchievedPoints.ToString();
        }

        public void Dispose()
        {
            if (_player.Creed != PlayerCreed.None)
            {
                _player.OnPointAchieved -= Player_OnPointAchieved;
            }
        }
    }
}
