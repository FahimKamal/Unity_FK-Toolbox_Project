# Unity_Popup_Manager
Popup Manager is a small package that will help with debugging in mobile games. To find out if a specific task or method is called or not. Just like how we use Debug() method in console. You will find a *.unitypackage file in here just import it in your project and add the Popup manager Prefab in your main scene. Then use the bellow method call to use the popup anywhere in your game. You only have to add the prefab in main scene.

See below example to know how to use this. Also you will find a sample scene which will demonstrate of it's usage.

## Usage

```c#
PopupManager.Instance.ShowPopup("Notification", "Button pressed from button 1");
```
