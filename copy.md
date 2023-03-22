# FK-Toolbox
These are some set of tools that I have created to smoothen my journey of game development with 
unity 3D. Maybe I'll add more in future as the need arise. Read below the names and usages of tools 
currently present in this package.
<details>
<summary>
# Unity Popup Manager
</summary>
 + jfkdsalfj
jf
</details>


Popup Manager is a small package that will help with debugging in mobile games. To find out if a specific 
task or method is called or not. Just like how we use `Debug.Log()` method in console. 
You will find a *.unitypackage file in here just import it in your project and add the 
Popup manager Prefab in your main scene. You will be able to use this 3 ways in your project.
+ <strong>Show Popup notification in different situation.
+ Print log in log window.
+ Print log in your Unity console.</strong>

See below example to know how to use this. Also you will find a sample scene which will 
demonstrate of it's usage.
</br></br>
![Example](https://imgur.com/XzEC37z.gif "Example")
## Usage

+ <strong>Initialization</strong> <br>
  Select the features that you want to use in your game. <br><br>
  ![Initialization](https://imgur.com/TZI7T3c.gif "Initialization")

```c#
    PopupManager.Instance.ShowPopup(
      description:"Button pressed from hello button", 
      title:"Notification", 
      onlyLog:true
    );
```
```c#
    PopupManager.Instance.ShowPopup("Test log text");
```
+ <strong>description:</strong> The message that you want to print in console/log and as popup body.
+ <strong>title:</strong>(Optional) The title for popup window.
+ <strong>onlyLog:</strong>(Optional) Set it to true if you only want to see it in console or log window in mobile device.
#### Note: In your final build just un-check `usePopup` and `useLogWindow` option to remove popups and log screen from your game. No need to remove or comment-out any code.
###### Note to self: For customize look of the Popup Manager in inspector. You have written some codes. Reference to those codes in future.
