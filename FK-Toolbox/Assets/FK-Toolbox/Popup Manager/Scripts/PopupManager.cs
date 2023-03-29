using System;
using Debug_Log_Manager;
using TriInspector;
using UnityEngine;

namespace Popup_Log_System
{
    public class PopupManager : MonoBehaviour
    {
        #region singleton

        private static PopupManager _instance;
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion
        
        #region Variables

        [Tooltip(
            "This event receives messages from different objects and delivers them here. Make sure to set it always.")]
        [Title("Messenger Event"), SerializeField]
        [Required]
        [LabelText("Message Receiver"), Indent]
        private PopupEvent popupEvent;

        [Title("PopUp Window Options"), Indent]
        [Tooltip("Un-tick if you don't want to show popup on screen")]
        [SerializeField]
        private bool usePopup = true;

        [Tooltip("Set time in seconds")] [SerializeField, EnableIf(nameof(usePopup)), Indent]
        private float popupDuration = 3f;

        [SerializeField, EnableIf(nameof(usePopup)), Indent] [Required, AssetsOnly]
        private GameObject popupPrefab;

        [Title("Log Window Options")]
        [Tooltip("Un-tick if you don't want to show log panel.")]
        [LabelText("Use Log Window"), Indent]
        [SerializeField]
        [OnValueChanged(nameof(IfUseLog))]
        private bool useLog = false;

        private void IfUseLog()
        {
            logDisplayManger.SetActive(useLog);
        }

        [SerializeField, EnableIf(nameof(useLog))] [Required, SceneObjectsOnly, Indent]
        private GameObject logDisplayManger;

        #endregion
        
        
        #region Initialization

        private void OnEnable()
        {
            if (popupEvent != null)
                popupEvent.onEventRaised.AddListener(Call);
            else
                Debug.LogError("Popup event object is not set in listener:" + gameObject.name);
        }

        private void OnDisable()
        {
            if (popupEvent != null)
                popupEvent.onEventRaised.RemoveListener(Call);
            else
                Debug.LogError("Popup event object is not set in listener:" + popupEvent.name);
        }

        private void Call(Messenge message)
        {
            ShowPopup(message.description, message.title, message.onlyLog);
        }

        #endregion
        
        /// <summary>
        /// Show popup on screen and print log.
        /// </summary>
        /// <param name="description">Description of your message.</param>
        /// <param name="title">Title of Popup.</param>
        /// <param name="onlyLog">Set true if you only want to print log.</param>
        private void ShowPopup(string description, string title = "Popup", bool onlyLog = false)
        {
            Debug.Log(description);
            if (useLog) logDisplayManger.GetComponent<LogDisplayManager>().Log(description);

            if (onlyLog) return;

            if (!usePopup) return;

            var popup = Instantiate(popupPrefab, transform);
            popup.GetComponent<SetPopup>().SetPopupData(title, description, popupDuration);
        }
    }
}