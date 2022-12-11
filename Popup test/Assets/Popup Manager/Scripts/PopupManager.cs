using System;
using System.Collections;
using System.Collections.Generic;
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

    public void ShowPopup(string title, string description)
    {
        if (!isPopupActive) return;
        
        var popup = Instantiate(popupPrefab, transform);
        popup.GetComponent<SetPopup>().SetPopupData(title, description, popupDuration);
    }
}
