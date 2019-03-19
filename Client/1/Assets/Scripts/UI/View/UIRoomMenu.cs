using System;

namespace Scripts.UI
{
    public class UIRoomMenu : BaseUI
    {
        private string WN_BTN_ConsoleGame = "WN_BTN_ConsoleGame";
        private string WN_BTN_JoinRoom = "WN_BTN_JoinRoom";
        private string WN_BTN_CreateRoom = "WN_BTN_CreateRoom";
        string game_name = "";

        public override void Open(params object[] _params)
        {
            game_name = _params[0].ToString();
            UIManager.GetInstance().RegisterClickEvent(WN_BTN_ConsoleGame, this,OnClickConsoleGame);
        }

        private void OnClickConsoleGame()
        {
            Logic.GameController.GetInstance().StartGame(game_name);
        }
    }
}
