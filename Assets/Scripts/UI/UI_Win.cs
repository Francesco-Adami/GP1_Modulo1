

using UnityEngine.SceneManagement;

public class UI_Win : UI_Template
{
    public void ReloadGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
