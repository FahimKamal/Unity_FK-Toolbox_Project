using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupTestScript : MonoBehaviour
{
    public void HelloButton()
    {
        PopupManager.Instance.ShowPopup( "Button pressed from hello button", "Notification", true);
    }
    
    public void HiButton()
    {
        PopupManager.Instance.ShowPopup( "Button pressed from hi button", "Notification");
    }
    
    public void ByeByeButton()
    {
        PopupManager.Instance.ShowPopup( "Button pressed from bye button", "Notification");
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
        PopupManager.Instance.ShowPopup("Game Closing.");
    }

    public void GoSecondPage()
    {
        SceneManager.LoadScene(1);
    }
    
    public void GoFirstPage()
    {
        SceneManager.LoadScene(0);
    }
}
