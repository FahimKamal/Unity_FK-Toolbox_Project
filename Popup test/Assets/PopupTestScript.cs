using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PopupTestScript : MonoBehaviour
{
    [SerializeField] private MessengerEvent messengerEvent;
    public void HelloButton()
    {
        Popup("Button pressed from hello button", "Notification", true);
    }
    
    public void HiButton()
    {
        Popup( "Button pressed from hi button", "Notification");
    }
    
    public void ByeByeButton()
    {
        Popup( "Button pressed from bye button", "Notification");
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
        Popup("Game Closing.");
    }

    public void GoSecondPage()
    {
        SceneManager.LoadScene(1);
    }
    
    public void GoFirstPage()
    {
        SceneManager.LoadScene(0);
    }

    private void Popup(string description,string title = "Popup", bool onlyLog = false)
    {
        messengerEvent.RaiseEvent(new Messenger(description, title, onlyLog));
    }
}
