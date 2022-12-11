using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTestScript : MonoBehaviour
{
    public void HelloButton()
    {
        PopupManager.Instance.ShowPopup("Notification", "Button pressed from hello button");
    }
    
    public void HiButton()
    {
        PopupManager.Instance.ShowPopup("Notification", "Button pressed from hi button");
    }
    
    public void ByeByeButton()
    {
        PopupManager.Instance.ShowPopup("Notification", "Button pressed from bye button");
    }
}
