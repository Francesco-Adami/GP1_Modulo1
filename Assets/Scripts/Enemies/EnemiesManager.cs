using UnityEngine;

public class EnemiesManager : MonoBehaviour
{


    #region singleton
    public static EnemiesManager instance { get; set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    #endregion

}
