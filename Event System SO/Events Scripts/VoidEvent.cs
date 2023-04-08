using EasyButtons;
using UnityEngine;
using UnityEngine.Events;

namespace Event_System_SO
{
    /// <summary>
    /// A ScriptableObject-derived class that represents a void event. This class is used to create events
    /// that have no associated data, and can be subscribed to and raised.
    /// It provides a single event named OnEventRaised that takes no parameters and is raised when the RaiseEvent method is called.
    /// </summary>
    [CreateAssetMenu(menuName = "Events/Void Event")]
    public class VoidEvent : ScriptableObject
    {
        /// <summary>
        /// The event that will be raised when the RaiseEvent method is called. This is a UnityAction delegate that takes no arguments.
        /// </summary>
        public UnityEvent onEventRaised;

        /// <summary>
        /// Raises the event by invoking the OnEventRaised delegate. If there are no subscribed event handlers, this method does nothing.
        /// </summary>
        [Button(Mode = ButtonMode.EnabledInPlayMode)]
        public void RaiseEvent()
        {
            onEventRaised.Invoke();
        }
    }
}
