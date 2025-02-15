

public class UI_MainMenu : UI_Template
{
    public void GoToInGameUI()
    {
        UIManager.instance.ShowUI(UIManager.GameUI.InGame);
    }
}
