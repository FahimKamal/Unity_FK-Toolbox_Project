using UnityEngine;
using UnityEngine.UI;

public class LogDisplayManager : MonoBehaviour
{
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
