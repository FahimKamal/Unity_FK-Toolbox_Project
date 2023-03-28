# FK-Toolbox

These are some set of tools that I have created to smoothen my journey of game development with
unity 3D. Maybe I'll add more in future as the need arise. Read below the names and usages of tools
currently present in this package.


## Dependencies

This package has below dependencies. You have to install them first.

+ **Easy Buttons** by Mads Bang Hoffensetz </br>

   ```http request
   https://github.com/madsbangh/EasyButtons.git#upm
   ```
+ **NaughtyAttributes** by Denis Rizov </br>

   ```http request
   https://github.com/dbrizov/NaughtyAttributes.git#upm
   ```
  

## Installation

Installation process for this package and all dependencies that are from github is same and written below.

+ Copy below URL:
   ```http request
  https://github.com/FahimKamal/Unity_FK-Toolbox.git#package
   ```
+ Open your `Package Manager` from Unity editor.
+ Press the `+` icon on the top left corner.
+ Select `Add Package from git URL...` option.
+ Paste the URL in the textbox and press `Add` button.


+ ## Custom Attributes
  <details>
   <summary>
    <span style="font-size: 23px"> <strong>Expand</strong> </span>
   </summary>

    + ### `[ShowIf]` Attribute
      I have taken this solution from a StackOverFlow answer. The link to the question is:
      [here](https://stackoverflow.com/questions/58441744/how-to-enable-disable-a-list-in-unity-inspector-using-a-bool "How to enable/disable a List in Unity inspector using a bool?")

      <details>
      <summary>
        <span style="font-size: 17px"> <strong>Usage</strong> </span>
      </summary>

        + Using a field to hide/show another field:

      ```c#
      public bool showHideList = false; 
      [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(showHideList))]
      public string aField = "item 1";
      ```
      ![hide/show a field](https://gyazo.com/7aa9ecb607415d71bf5c5948f856eab1.gif "Hide/show a field")

        + Using a field to enable/disable another field:

      ```c#
      public bool enableDisableList = false;
     
      [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.And, 
      nameof(enableDisableList))]
      public string anotherField = "item 2";
      ```
      ![Enable/Disable a field](https://gyazo.com/f94d76702f32adf4d6a22eccaf5a0d4a.gif "enable/disable a field")

        + Using multiple conditions on the same field:

      ```c#
      public bool condition1;    
      public bool condition2;    
      [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.And, nameof(condition1), 
      nameof(condition2))]    
      public string oneLastField= "last field";
      ```
      ![hide/show a field](https://gyazo.com/832b043e065741a170f9a5cbc42abe10.gif "Use multiple conditions on a same field")

        + Using a method to get a condition value:

      ```c#
      [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.And,nameof(CalculateIsEnabled))]
      public string yetAnotherField = "one more";    
      public bool CalculateIsEnabled()    
      {
          return true;    
      }
      ```
      ![Using a method to get a condition value](https://i.gyazo.com/f87aae44ff47e046b5f3dc5b3e26c8f9.png "Using a method to get a condition value")

      </details>

    + ### `[RequireReference]` Attribute

      There are certain fields in your scripts like `GameObject`, `Transform`, `Prefab` that can't be `null`.
      Otherwise it will throw an error while running the game. In that places you can add this attribute
      to give you an warning, to set that fields with appropriate object reference.

      <details>
       <summary>
         <span style="font-size: 17px"> <strong>Usage</strong> </span>
       </summary>

        + Add the attribute like below example.
          ```c#
          [RequireReference]
          [SerializeField] private PopupEvent popupEvent;
          ```
          You will see something like this in inspector. <br>
          ![](https://imgur.com/SocVr3A.gif "Initialization")

        + (Optional) You can also add you own warning text.
          ```c#
          [RequireReference]
          [SerializeField] private PopupEvent popupEvent;
          ```
          You will see something like this in inspector. <br>
          ![](https://imgur.com/QWWAEo1.gif "Initialization")
      </details>

  + ### `[Expandable]` Attribute

    This Attribute is taken from **NaughtyAttributes** created by Denis Rizov. Visit the git repo
    [here](https://github.com/dbrizov/NaughtyAttributes.git "NaughtyAttributes").

    <details>
     <summary>
       <span style="font-size: 17px"> <strong>Usage</strong> </span>
     </summary>

      + Make scriptable objects expandable.
        ```c#
         public class NaughtyComponent : MonoBehaviour
         {
             [Expandable]
             public ScriptableObject scriptableObject;
         }
        ```
        You will see something like this in inspector. <br>
        ![](https://imgur.com/OVuBKEK.gif "Expandable_Inspector")

    </details>
    
  </details>


+ ## Event System with Scriptable Object
  This is a flexible and easy-to-use event system for Unity that allows you to define events with 
  custom data types. It decouples event logic from game objects and scripts, making it easier to maintain
  and refactor code. It includes a base event class and a void event class, and events can be raised by 
  calling the RaiseEvent method. Several example classes and a demo scene are included to demonstrate
  the system's functionality. </br> </br>

   <details>
     <summary>
       <span style="font-size: 23px"> <strong>Expand</strong> </span>
     </summary>
  
    You will find some build-in type of events that you can use for your different use case.
    + <strong>Void Event : </strong> You can raise this event for your specific events and all other scripts 
      that has subscribed to this event will listen and execute their specific tasks. No data will be passed on.
    + <strong>Int Event : </strong> Will work same as <strong>Void Event</strong> only you will be able to passed on
      a `int` value.
    + <strong>String Event : </strong> Will work same as <strong>Void Event</strong> only you will be able to passed on
      a `string` value.
    + <strong>Custom Event : </strong> Will work same as <strong>Void Event</strong> but with more custom data type. 
      by extending the `BaseEvent<T>` class you can passed on other data types even custom data class.
      See use case section to understand how to do that.
      
    <details>
      <summary>
        <span style="font-size: 17px"> <strong>Usage</strong></span>
      </summary>
  
    + ### `[Void Event]`
      + #### Initialization: 
        + Right Click in your `Project` Window and select.</br>
        Create -> Events -> Void Event. Give it a name and save it.
        + In your Broadcaster Script: Write these lines to reference the event and drag-n-drop the event from your assets folder.
        ```c#
         [RequireReference]
         [SerializeField] private VoidEvent damageEvent;
        ```
        + Now to raise the event write these lines of code:
        ```c#
         private void OnCollisionEnter2D(Collision2D col)
         {
            if (damageEvent != null)
            {
                damageEvent.RaiseEvent();
            }
         }
        ```
        + Now in your Listener Scripts for example your UI controller : Write these lines to reference the event and drag-n-drop the event from your assets folder.
        ```c#
        [RequireReference]
        [SerializeField] private VoidEvent damageEvent;
        
        ...
        
        private void OnEnable()
        {
          damageEvent.onEventRaised.AddListener(OnEventRaised);
        }
        
        private void OnDisable()
        {
          damageEvent.onEventRaised.RemoveListener(OnEventRaised);
        }
        
        private void OnEventRaised()
        {
          messageBox.text = "Player is collide with an enemy";
          ...
          // Other codes.
          ...
        }
        
        ...
        ```
        + Whatever you have in your `OnEventRaised()` method will be executed when the event is raised from 
          the Broadcaster script.

    + ### `[Int Event]`
      + #### Initialization:
          + Right Click in your `Project` Window and select.</br>
            Create -> Events -> Int Event. Give it a name and save it.
          + In your Broadcaster Script: Write these lines to reference the event and drag-n-drop the event from your assets folder.
        ```c#
         [RequireReference]
         [SerializeField] private IntEvent damageEvent;
        ```
          + Now to raise the event write these lines of code: Value of `damageAmount` will ge passed on as parameter.
        ```c#
         ...
         int damageAmount = 10;
         ... 
        
         private void OnCollisionEnter2D(Collision2D col)
         {
            if (damageEvent != null)
            {
                damageEvent.RaiseEvent(damageAmount);
            }
         }
        ```
        + Now in your Listener Scripts for example your UI controller : Write these lines to reference the event and drag-n-drop the event from your assets folder.
        ```c#
        [RequireReference]
        [SerializeField] private IntEvent damageEvent;
        
        ...
        
        private void OnEnable()
        {
          damageEvent.onEventRaised.AddListener(OnEventRaised);
        }
        
        private void OnDisable()
        {
          damageEvent.onEventRaised.RemoveListener(OnEventRaised);
        }
        
        private void OnEventRaised(int damageAmount)
        {
          messageBox.text = "Player took damage of" + damageAmount;
          ...
          // Other codes.
          ...
        }
        
        ...
        ```
        + In this case `damageAmount` will be carried here from Broadcaster and you can use the value as you need.

    + ### `[Custom Event]`
      + <strong>Initialization: </strong> Maybe you need to send some other data type like `float` or maybe some other
        custom data class. You can do that by extending `BaseEvent<T>` class.
      + Let's create a Event that will passed on `float` value. See below code:
      ```c#
      [CreateAssetMenu(menuName = "Events/Float Event")]
      public class FloatEvent : BaseEvent<float>
      {
    
      }
      ```
      + That's it. Now use it same way you would use `Int Event`.
      + Let's Create a Event that will passed on a data class. See below code:
      ```C#
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
      ```
      + Above Event class has be used by the `Popup Manager`. It's that simple. You can use above event same way you would
        use `IntEvent` or `FloatEvent`.

  </details>
    
  
   </details>


+ ## Popup Manager
  Popup Manager is a small package that can be useful for debugging in mobile games. Its purpose is to help determine 
  if a specific task or method is being called. It works similar to how we use the `Debug.Log()` method 
  in the console.

  To use Popup Manager, simply add the Popup Manager Prefab to your scene. You can use it in three ways:

    + <strong>Show Popup notification in different situation.
    + Print log in log window.
    + Print log in your Unity console.</strong>
      </br></br>
  <details>
    <summary>
      <span style="font-size: 23px"> <strong>Expand</strong> </span>
    </summary>

  See below example to know how to use this. Also you will find a sample scene which will
  demonstrate of it's usage.
  </br></br>
  ![Example](https://imgur.com/XzEC37z.gif "Example")

    <details>
      <summary>
        <span style="font-size: 17px"> <strong>Usage</strong></span>
      </summary>

    <strong>Initialization</strong> <br>
    + Add the `Popup Manager` prefab into your scene.</br>
    + Select the features that you want to use in your game.
    + Make sure `Message Receiver Event` is set. You will find that in resource folder.<br><br>
      ![Initialization](https://imgur.com/BBJH9ps.gif "Initialization")<br><br>
    + Create a new variable like bellow, in your scripts where you want to call and show Popup/log.
      ```c#
      [RequireReference]
      [SerializeField] private PopupEvent popupEvent;
      ```
    + Set reference to `PopupEvent` from inspector. You will find that in resource folder.<br> <br>
      ![Initialization](https://imgur.com/SocVr3A.gif "Initialization")<br><br>
    + Now each time you need to show popup or log text call below method from `popupEvent`.
      ```c#
      popupEvent.ShowPopup(description:"Button pressed from hello button", title:"Notification");
      ```
      ```c#
      popupEvent.ShowPopup("Game Closing.");
      ```
      ```c#
      popupEvent.ShowPopup("Data saved to cloud", onlyLog:true);
      ```
        + <strong>description:</strong> The message that you want to print in console/log and as popup body.
        + <strong>title:</strong>(Optional) The title for popup window.
        + <strong>onlyLog:</strong>(Optional) Set it to true if you only want to see it in console or log window in
          mobile device.
    </details>

  #### Note: In your final build just un-check `usePopup` and `useLogWindow` option to remove popups and log screen from your game. No need to remove or comment-out any code.
  ###### Note to self: For customize look of the Popup Manager in inspector. You have written some codes. Reference to those codes in future.

  </details>


