using Custom_Attribute;
using Debug_Log_Manager;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Popup_Log_System
{
    public class PopupManager : MonoBehaviour
{
    #region Initialization

    private void OnEnable()
    {
        if (popupEvent != null) 
        {
            popupEvent.onEventRaised.AddListener(Call);
        }
        else
        {
            Debug.LogError("Popup event object is not set in listener:" + gameObject.name);
        }
    }

    private void OnDisable()
    {
        if (popupEvent != null)
        {
            popupEvent.onEventRaised.RemoveListener(Call);
        }
        else
        {
            Debug.LogError("Popup event object is not set in listener:" + popupEvent.name);
        }
    }

    private void Call(Messenge message)
    {
        ShowPopup(message.description, message.title, message.onlyLog);
    }

    #endregion
    
    #region Seter and Geters

    public bool UsePopup
    {
        get => usePopup;
        set => usePopup = value;
    }
    public bool UseLog
    {
        get => useLog;
        set => useLog = value;
    }

    #endregion

    #region Variables

    [Tooltip("This event receives messages from different objects and delivers them here. Make sure to set it always.")]
    [SerializeField] 
    [RequireReference("This event receives messages from different objects and delivers them here. Make sure to set it with proper reference always.")]
    private PopupEvent popupEvent;
    
    [Tooltip("Un-tick if you don't want to show popup on screen")]
    [SerializeField] 
    private bool usePopup = true;
    [Tooltip("Set time in seconds")]
    [SerializeField, ShowIf(ActionOnConditionFail.JUST_DISABLE, ConditionOperator.AND, nameof(usePopup))] 
    private float popupDuration = 3f;
    [SerializeField, ShowIf(ActionOnConditionFail.JUST_DISABLE, ConditionOperator.AND, nameof(usePopup))]
    [RequireReference("Set the popup prefab ref here. Can't be left empty.")]
    private GameObject popupPrefab;
    
    [Space (10)]
    [Tooltip("Un-tick if you don't want to show log panel.")]
    [SerializeField] 
    private bool useLog = false;
    
    [SerializeField, ShowIf(ActionOnConditionFail.JUST_DISABLE, ConditionOperator.AND, nameof(useLog))] 
    // [RequireReference("Get the child object of save name and set it here.")]
    [RequireReference]
    private GameObject logDisplayManger;

    #endregion
    
    public void ToggleLogPanel()
    {
        if (logDisplayManger != null)
        {
            logDisplayManger.SetActive(useLog);
        }
        
    }
    
    /// <summary>
    /// Show popup on screen and print log.
    /// </summary>
    /// <param name="description">Description of your message.</param>
    /// <param name="title">Title of Popup.</param>
    /// <param name="onlyLog">Set true if you only want to print log.</param>
    private void ShowPopup(string description,string title = "Popup", bool onlyLog = false)
    {
        Debug.Log(description);
        if (useLog) logDisplayManger.GetComponent<LogDisplayManager>().Log(description);
        
        if (onlyLog) return;
        
        if (!usePopup) return;
        
        var popup = Instantiate(popupPrefab, transform);
        popup.GetComponent<SetPopup>().SetPopupData(title, description, popupDuration);
    }

}

    #region Customize view in Inspector

    #if UNITY_EDITOR
    [CustomEditor(typeof(PopupManager))]
    public class MyScriptEditor : Editor
    {
        
        private SerializedProperty _messageReceiverEvent;
        private SerializedProperty _usePopup;
        private SerializedProperty _popupDuration;
        private SerializedProperty _popupPrefab;
        private SerializedProperty _useLog;
        private SerializedProperty _logDisplayManger;

        private void OnEnable()
        {
            _messageReceiverEvent = serializedObject.FindProperty("popupEvent");
            _usePopup = serializedObject.FindProperty("usePopup");
            _popupDuration = serializedObject.FindProperty("popupDuration");
            _popupPrefab = serializedObject.FindProperty("popupPrefab");
            _useLog = serializedObject.FindProperty("useLog");
            _logDisplayManger = serializedObject.FindProperty("logDisplayManger");
        }

        public override void OnInspectorGUI()
        {
            // DrawDefaultInspector();
            
            
            serializedObject.Update();
            var myScript = (PopupManager)target;
            if (myScript.UseLog)
                myScript.ToggleLogPanel();
            else
                myScript.ToggleLogPanel();

            EditorGUILayout.LabelField("Messenger Event", EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_messageReceiverEvent, new GUIContent("Message Receiver Event"));

            EditorGUI.indentLevel--;
            EditorGUILayout.Separator();
            
            EditorGUILayout.LabelField("PopUp Window Options", EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_usePopup, new GUIContent("Use Popup"));

            if (!myScript.UsePopup)
            {
                EditorGUILayout.HelpBox(
                    "Check if you want to see popups during game play", 
                    MessageType.Info);
            }
            EditorGUILayout.PropertyField(_popupDuration, new GUIContent("Popup Duration in Sec"));
            EditorGUILayout.PropertyField(_popupPrefab, new GUIContent("Popup Prefab"));
            
            EditorGUI.indentLevel--;
            EditorGUILayout.Separator();
            
            EditorGUILayout.LabelField("Log Window Options", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_useLog, new GUIContent("Use Log Window"));
            if (!myScript.UseLog)
            {
                EditorGUILayout.HelpBox(
                    "Check if you want to see Log window during game play.", 
                    MessageType.Info);
            }
            EditorGUILayout.PropertyField(_logDisplayManger, new GUIContent("Log Display Manager"));
            
            EditorGUI.indentLevel--;
            EditorGUILayout.Separator();
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif

    #endregion
}

