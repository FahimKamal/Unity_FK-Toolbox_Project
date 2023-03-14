using System.Collections;
using Events_Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BotMessageBox : MonoBehaviour
{
    [FormerlySerializedAs("EventWithInt")] [FormerlySerializedAs("intEvent")] [SerializeField] private IntEvent eventWithInt;
    [SerializeField] private CustomClassEvent classEvent;
    [SerializeField] private Text messageBox;

    private void OnEnable()
    {
        eventWithInt.onEventRaised.AddListener(OnEventRaised); 
        if (classEvent != null)
        {
            classEvent.onEventRaised.AddListener(DamageTaken);
        }
        else
        {
            throw new System.Exception("No Reference set yet");
        }
    }

    private void DamageTaken(DataClass dataClass)
    {
        messageBox.text += "\nDamage taken: " + dataClass.damage;
        StartCoroutine(ResetMessagebox());
    }

    private void OnDisable()
    {
        eventWithInt.onEventRaised.RemoveListener(OnEventRaised);
        if (classEvent != null)
        {
            classEvent.onEventRaised.RemoveListener(DamageTaken);
        }
        else
        {
            throw new System.Exception("No Reference set yet");
        }
    }

    private void OnEventRaised(int id)
    {
        messageBox.text += "\nPlayer is collided with Tree id: " + id;
        StartCoroutine(ResetMessagebox());
    }

    private IEnumerator ResetMessagebox()
    {
        yield return new WaitForSeconds(3.3f);
        messageBox.text = "Message Box";
    }
}
