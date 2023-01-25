using UnityEngine;
using UnityEngine.UI;

public class LogDisplayManager : MonoBehaviour
{
    public static LogDisplayManager Instance;

    #region singleton

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    
}
