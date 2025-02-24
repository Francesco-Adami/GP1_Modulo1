using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : UI_Template
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI newWaveCooldown;

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
        if (GameManager.instance.cooldownTimer <= 0)
        {
            newWaveCooldown.text = "";
        }
        else
        {
            newWaveCooldown.text = "Next Wave in:\n" + Mathf.FloorToInt(GameManager.instance.cooldownTimer);
        }

        slider.value = PlayerController.instance.loadedBullet > slider.maxValue ? slider.maxValue : PlayerController.instance.loadedBullet;
    }
}
