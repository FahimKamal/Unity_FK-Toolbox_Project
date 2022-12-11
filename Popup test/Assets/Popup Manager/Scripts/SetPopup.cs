using System.Collections;
using TMPro;
using UnityEngine;

public class SetPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Animation animationCom;
    
    [SerializeField] private float popupDuration = 3f;

    private void Start()
    {
        StartCoroutine(DestroyPopup());
    }

    public void SetPopupData(string title, string description, float popupDuration)
    {
        this.popupDuration = popupDuration;
        this.title.text = title;
        this.description.text = description;
        animationCom.Play("Popup Animation on");
        CallBackManager.Instance.onPopupOpened?.Invoke();
    }
    
    // Destroy the popup after 2 seconds
    private IEnumerator DestroyPopup()
    {
        yield return new WaitForSeconds(popupDuration);
        animationCom.Play("Popup Animation off");
        yield return new WaitForSeconds(0.5f);
        CallBackManager.Instance.onPopupClosed?.Invoke();
        
        Destroy(gameObject);
    }
}
