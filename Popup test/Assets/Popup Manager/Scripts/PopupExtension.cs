using UnityEngine;

namespace Popup_Log_System
{
    public static class PopupExtension 
    {
        /// <summary>
        /// Show popup on screen and print log.
        /// </summary>
        /// <param name="popupEvent"></param>
        /// <param name="description">Description of your message.</param>
        /// <param name="title">Title of Popup.</param>
        /// <param name="onlyLog">Set true if you only want to print log.</param>
        public static void ShowPopup(this PopupEvent popupEvent, string description, string title = "Popup",
            bool onlyLog = false)
        {
            if (popupEvent != null)
            {
                popupEvent.RaiseEvent(new Messenge(description, title, onlyLog));
            }
            else
            {
                Debug.Log("Popup event object is not set in broadcaster:");
            }
        
        }
    }
}
