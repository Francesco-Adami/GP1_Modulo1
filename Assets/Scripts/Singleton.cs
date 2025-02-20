using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();

                if (_instance == null)
                {
                    throw new System.Exception("No object found with type " + typeof(T));
                }
            }
            return _instance;
        }
    }

    //protected void Awake()
    //{
    //    if (_instance != null && _instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        _instance = this as T;
    //    }
    //}
}
