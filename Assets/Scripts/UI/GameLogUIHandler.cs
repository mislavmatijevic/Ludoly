using TMPro;

namespace Assets.Scripts.UI
{
    public class GameLogUIHandler
    {
        public TextMeshProUGUI GameLogText { get; set; }

        public GameLogUIHandler(TextMeshProUGUI gameLogText)
        {
            GameLogText = gameLogText;
            GameLogText.text = "";
            Game.Instance.OnMessageEvent += Instance_OnMessageEvent;
        }

        private void Instance_OnMessageEvent(string message)
        {
            GameLogText.text = message;
        }
    }
}
