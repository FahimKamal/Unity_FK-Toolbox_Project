using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PopupManager : MonoBehaviour
{
    #region singleton

    public static PopupManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        if (FindObjectOfType<CallBackManager>() != null) return;
        var callBackManager = new GameObject("CallBackManager");
        callBackManager.AddComponent<CallBackManager>();
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
    
    [Tooltip("Un-tick if you don't want to show popup on screen")]
    [SerializeField] 
    private bool usePopup = true;
    [Tooltip("Set time in seconds")]
    [SerializeField, ShowIf(ActionOnConditionFail.JUST_DISABLE, ConditionOperator.AND, nameof(usePopup))] 
    private float popupDuration = 3f;
    [SerializeField, ShowIf(ActionOnConditionFail.JUST_DISABLE, ConditionOperator.AND, nameof(usePopup))] 
    private GameObject popupPrefab;
    
    [Space (10)]
    [Tooltip("Un-tick if you don't want to show log panel.")]
    [SerializeField] 
    private bool useLog = true;
    
    [SerializeField, ShowIf(ActionOnConditionFail.JUST_DISABLE, ConditionOperator.AND, nameof(useLog))] 
    private GameObject logDisplayManger;

    public void ToggleLogPanel()
    {
        logDisplayManger.SetActive(useLog);
    }
    
    /// <summary>
    /// Show popup on screen and print log.
    /// </summary>
    /// <param name="description">Description of your message.</param>
    /// <param name="title">Title of Popup.</param>
    /// <param name="onlyLog">Set true if you only want to print log.</param>
    public void ShowPopup(string description,string title = "Popup", bool onlyLog = false)
    {
        Debug.Log(description);
        if (useLog) LogDisplayManager.Instance.Log(description);
        
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
    
    private SerializedProperty _usePopup;
    private SerializedProperty _popupDuration;
    private SerializedProperty _popupPrefab;
    private SerializedProperty _useLog;
    private SerializedProperty _logDisplayManger;

    private void OnEnable()
    {
        _usePopup = serializedObject.FindProperty("usePopup");
        _popupDuration = serializedObject.FindProperty("popupDuration");
        _popupPrefab = serializedObject.FindProperty("popupPrefab");
        _useLog = serializedObject.FindProperty("useLog");
        _logDisplayManger = serializedObject.FindProperty("logDisplayManger");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var myScript = (PopupManager)target;
        if (myScript.UseLog)
            myScript.ToggleLogPanel();
        else
            myScript.ToggleLogPanel();

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
