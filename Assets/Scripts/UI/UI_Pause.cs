using UnityEngine;

public class UI_Pause : UI_Template
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowInGameUI();
        }
    }

    public void ShowInGameUI()
    {
        UIManager.instance.ShowUI(UIManager.GameUI.InGame);
    }
}
