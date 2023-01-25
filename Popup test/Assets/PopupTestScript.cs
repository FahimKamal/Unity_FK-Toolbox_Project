using UnityEngine;

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
}
