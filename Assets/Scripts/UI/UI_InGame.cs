using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : UI_Template
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.maxValue = PlayerController.instance.maxLoadingTimer;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.instance.ShowUI(UIManager.GameUI.Pause);
        }

        slider.value = PlayerController.instance.loadedBullet > slider.maxValue ? slider.maxValue : PlayerController.instance.loadedBullet;
    }
}
