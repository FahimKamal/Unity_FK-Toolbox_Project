using System;
using Event_System_SO;
using UnityEngine;
[CreateAssetMenu(menuName = "Events/Messenger Event")]
public class MessengerEvent : BaseEvent<Messenger>
{
    
}


[Serializable]
public class Messenger
{
    public string description;
    public string title;
    public bool onlyLog;

    public Messenger(string description, string title, bool onlyLog)
    {
        this.description = description;
        this.title = title;
        this.onlyLog = onlyLog;
    }
}
