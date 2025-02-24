using UnityEngine;

public class UI_MainMenu : UI_Template
{
    public void GoToInGameUI()
    {
        UIManager.instance.ShowUI(UIManager.GameUI.InGame);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
