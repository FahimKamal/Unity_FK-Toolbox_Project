using EasyButtons;
using UnityEngine;
using UnityEngine.Events;

namespace Events_Scripts
{
    /// <summary>
    /// An abstract class for creating events that can be subscribed to and raised.
    /// This class inherits from Unity's ScriptableObject class and provides a generic implementation for
    /// raising events that can be used by derived classes. The generic type parameter T represents
    /// the type of data that is passed to the event handlers when the event is raised.
    ///
    ///
    /// Add this line on top after inheriting this class: [CreateAssetMenu(menuName = "Events/[name] Event")]
    /// </summary>
    /// <typeparam name="T">The type of data that is passed to the event handlers when the event is raised.</typeparam>

    public abstract class BaseEvent <T> : ScriptableObject
    {
        /// <summary>
        /// The event that will be raised when the RaiseEvent method is called. This is a UnityAction delegate that takes a single argument of type T.
        /// </summary>
        public UnityEvent<T> onEventRaised;

        /// <summary>
        /// Raises the event and passes the specified parameter to any subscribed event handlers. If there are no subscribed event handlers, this method does nothing.
        /// </summary>
        /// <param name="value">The parameter to pass to the event handlers.</param>
        [Button(Mode = ButtonMode.EnabledInPlayMode, Expanded = true)]
        public void RaiseEvent(T value)
        {
            onEventRaised.Invoke(value);
        }
    }
}
