using Custom_Attribute;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PopupTestScript : MonoBehaviour
{
    [RequireReference]
    [SerializeField] private PopupEvent popupEvent;
    public void HelloButton()
    {
        popupEvent.ShowPopup("Button pressed from hello button", "Notification", true);
    }
    
    
    public void HiButton()
    {
        popupEvent.ShowPopup( "Button pressed from hi button", "Notification");
    }
    
    public void ByeByeButton()
    {
        popupEvent.ShowPopup( "Button pressed from bye button", "Notification");
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
        popupEvent.ShowPopup("Game Closing.");
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
