using UnityEngine;
using UnityEngine.UI;

public class LogController : MonoBehaviour
{
    [SerializeField] private Animation animationCom;
    [SerializeField] private GameObject buttonImg;
    [SerializeField] private Text logTextBox;

    private bool _panelOpen;
    private int _logID = 1;

    private void OpenPanel()
    {
        animationCom.Play("Panel Open");
        var scale = buttonImg.transform.localScale;
        scale.x = -1;
        buttonImg.transform.localScale = scale;
    }

    private void ClosePanel()
    {
        animationCom.Play("Panel Close");
        var scale = buttonImg.transform.localScale;
        scale.x = 1;
        buttonImg.transform.localScale = scale;
    }

    /// <summary>
    /// Toggle the log panel open and close.
    /// </summary>
    public void LogPanelToggle()
    {
        if (_panelOpen) ClosePanel();
        else OpenPanel();
        
        _panelOpen = !_panelOpen;
    }

    /// <summary>
    /// Set the log text in the log display panel. 
    /// </summary>
    /// <param name="logTxt">Text to display.</param>
    public void SetLogText(string logTxt)
    {
        logTextBox.text += "Log [" + _logID++ + "]: " + logTxt + "\n";
    }
}
