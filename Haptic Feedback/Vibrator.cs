using UnityEngine;

namespace Haptic
{
    public enum HapticEffect
    {
        Little = 30,
        Small = 50,
        Medium = 100,
        High = 250
    }

    public static class Vibrator
    {
#if UNITY_ANDROID && !UNITY_EDITOR
    private static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    private static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    private static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
        private static AndroidJavaClass unityPlayer;
        private static AndroidJavaObject currentActivity;
        private static AndroidJavaObject vibrator;
#endif

        public static void Vibrate(long milliseconds = 250)
        {
            Debug.Log("Vibrating for: " + milliseconds + "milliseconds");
            if (IsAndroid())
                vibrator.Call("vibrate", milliseconds);
            else
            {
                #if UNITY_STANDALONE_WIN
                Handheld.Vibrate();
                #endif
            }
        }
    
        public static void Vibrate(HapticEffect effect = HapticEffect.High)
        {
            Debug.Log("Vibrating for: " + (long) effect + "milliseconds");
            if (IsAndroid())
                vibrator.Call("vibrate", (long) effect);
            else
            {
                #if UNITY_STANDALONE_WIN
                Handheld.Vibrate();
                #endif
                
            }
        }

        public static void Cancel()
        {
            if (IsAndroid()) vibrator.Call("cancel");
        }

        private static bool IsAndroid()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
            return false;
#endif
        }
    }
}