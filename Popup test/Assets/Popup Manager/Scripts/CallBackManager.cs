using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallBackManager : MonoBehaviour
{
    public static CallBackManager Instance;

    #region Singleton

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    
    }

    #endregion
    
    public UnityEvent onPopupClosed = new UnityEvent();
    public UnityEvent onPopupOpened = new UnityEvent();
    
    public UnityEvent onCoinCollected = new UnityEvent();
}
