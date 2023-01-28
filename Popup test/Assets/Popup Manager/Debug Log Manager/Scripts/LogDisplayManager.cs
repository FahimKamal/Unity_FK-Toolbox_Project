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

    [SerializeField] private LogController _controller;

    /// <summary>
    /// Display the log text in log panel.
    /// </summary>
    /// <param name="logTxt">Text to display in log.</param>
    public void Log(string logTxt)
    {
        _controller.SetLogText(logTxt);
    }

}
