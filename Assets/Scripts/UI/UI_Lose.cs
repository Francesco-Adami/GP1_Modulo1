

using UnityEngine.SceneManagement;

public class UI_Lose : UI_Template
{
    public void ReloadGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
