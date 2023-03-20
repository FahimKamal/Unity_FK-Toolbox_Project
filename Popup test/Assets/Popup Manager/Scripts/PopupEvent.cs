using System;
using Event_System_SO;
using UnityEngine;
[CreateAssetMenu(menuName = "Events/Messenger Event")]
public class PopupEvent : BaseEvent<Messenge>
{
}


[Serializable]
public class Messenge
{
    public string description;
    public string title;
    public bool onlyLog;

    public Messenge(string description, string title, bool onlyLog)
    {
        this.description = description;
        this.title = title;
        this.onlyLog = onlyLog;
    }
}
