using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    public float newStageCooldown = 3f;
    public bool bossDefeated = false;

    [HideInInspector] public float cooldownTimer;
    [HideInInspector] public bool isGameActive;

    private int stageIndex = -1;
    private bool allStagesFinished = false;


    private void Update()
    {
        if (UIManager.instance.GetCurrentActiveUI() == UIManager.GameUI.InGame) isGameActive = true;
        else { isGameActive = false; return; }

        if (PlayerController.instance.health <= 0)
        {
            isGameActive = false;
            UIManager.instance.ShowUI(UIManager.GameUI.Lose);
            return;
        }

        if (bossDefeated)
        {
            isGameActive = false;
            UIManager.instance.ShowUI(UIManager.GameUI.Win);
        }

        if (isGameActive && stageIndex < 0)
        {
            stageIndex++;
            StartCoroutine(StartNextWave());
        }

        if (allStagesFinished)
        {
            allStagesFinished = false;
            StartCoroutine(StartBossFight());
        }

        if (stageIndex >= StageManager.instance.stages.Count) return;

        if (isGameActive && StageManager.instance.IsStageFinished(stageIndex) && DestroyedAllEnemies())
        {
            stageIndex++;
            if (stageIndex < StageManager.instance.stages.Count)
                StartCoroutine(StartNextWave());
            else
                allStagesFinished = true;
        }
    }

    private bool DestroyedAllEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        if (enemies == null || enemies.Length == 0) return true;
        return false;
    }

    private IEnumerator StartNextWave()
    {
        cooldownTimer = newStageCooldown + 1;

        while (cooldownTimer > 0)
        {
            if (UIManager.instance.GetCurrentActiveUI() == UIManager.GameUI.InGame)
                cooldownTimer -= Time.deltaTime;

            yield return null;
        }
        StageManager.instance.SpawnStage(stageIndex);
    }

    private IEnumerator StartBossFight()
    {
        cooldownTimer = newStageCooldown + 1;

        while (cooldownTimer > 0)
        {
            if (UIManager.instance.GetCurrentActiveUI() == UIManager.GameUI.InGame)
                cooldownTimer -= Time.deltaTime;

            yield return null;
        }
        Instantiate(StageManager.instance.bossPrefab);
    }

    public bool IsGameActive() { return isGameActive; }
    public void SetIsGameActive(bool isGameActive) { this.isGameActive = isGameActive; }
}
