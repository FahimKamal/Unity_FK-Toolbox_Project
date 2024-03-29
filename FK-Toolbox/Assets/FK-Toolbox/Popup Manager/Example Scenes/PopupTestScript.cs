using Custom_Attribute;
using Popup_Log_System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupTestScript : MonoBehaviour
{
    //[RequireReference, Title("something")]
    [SerializeField] private PopupEvent popupEvent;
    public void HelloButton()
    {
        popupEvent.ShowPopup(description:"Button pressed from hello button", title:"Notification", onlyLog:true);
        PopupManager.Instance.ShowPopup(description:"Button pressed from hello button singleton", title:"Notification", onlyLog:true);
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
        SceneManager.LoadScene("PopupExample 2");
    }
    
    public void GoFirstPage()
    {
        SceneManager.LoadScene("PopupExample");
    }
}
