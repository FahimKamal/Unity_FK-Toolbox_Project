using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    #region singleton

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
    

    [SerializeField] private GameObject popupPrefab;
    [Space (10)]
    
    [Tooltip("Set time in seconds")]
    [SerializeField] private float popupDuration = 3f;
    
    [Tooltip("Un-tick if you don't want to show popup on screen")]
    [SerializeField] private bool isPopupActive = true;

    /// <summary>
    /// Show popup on screen and print log.
    /// </summary>
    /// <param name="description">Description of your message.</param>
    /// <param name="title">Title of Popup.</param>
    /// <param name="onlyLog">Set true if you only want to print log.</param>
    public void ShowPopup(string description,string title = "Popup", bool onlyLog = false)
    {
        Debug.Log(description);
        if (onlyLog) return;
        if (!isPopupActive) return;
        var popup = Instantiate(popupPrefab, transform);
        popup.GetComponent<SetPopup>().SetPopupData(title, description, popupDuration);
    }
}
