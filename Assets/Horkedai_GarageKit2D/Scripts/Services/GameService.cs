using UnityEngine;

public class GameService : MonoBehaviour
{
    public static GameService Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance  = this;
        DontDestroyOnLoad(gameObject);
    }


    //Code:
    public TuningCatalogSO catalog;
}
