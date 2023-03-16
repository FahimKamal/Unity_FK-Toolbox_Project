using System.Collections;
using Event_System_SO;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TopMessageBox : MonoBehaviour
{
    [FormerlySerializedAs("EventWithString")] [FormerlySerializedAs("collideEventWithString")] [SerializeField] private StringEvent eventWithString;
    [SerializeField] private Text messageBox;

    private void OnEnable()
    {
        eventWithString.onEventRaised.AddListener(OnEventRaised);
    }
    private void OnDisable()
    {
        eventWithString.onEventRaised.AddListener(OnEventRaised);
    }

    private void OnEventRaised(string message)
    {
        messageBox.text = "Player is collide with: " + message;
        StartCoroutine(ResetMessagebox());
    }

    private IEnumerator ResetMessagebox()
    {
        yield return new WaitForSeconds(3.3f);
        messageBox.text = "Message Box";
    }
}
