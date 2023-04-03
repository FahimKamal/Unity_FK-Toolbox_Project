using UnityEngine;

namespace SaveData
{
    public static class SaveData
    {
        public static void DeleteAll()
        {
            SaveDataManager.Instance.DeleteAll();
        }

        #region Player Pref

        public static void SetBool(string key, bool value)
        {
            SaveDataManager.Instance.SetBool(key, value);
        }

        public static void SetString(string key, string value)
        {
            SaveDataManager.Instance.SetString(key, value);
        }

        public static void SetInt(string key, int value)
        {
            SaveDataManager.Instance.SetInt(key, value);
        }

        public static void SetFloat(string key, float value)
        {
            SaveDataManager.Instance.SetFloat(key, value);
        }

        public static bool GetBool(string key, bool default_value = false)
        {
            return SaveDataManager.Instance.GetBool(key, default_value);
        }

        public static string GetString(string key, string default_value = "")
        {
            return SaveDataManager.Instance.GetString(key, default_value);
        }

        public static int GetInt(string key, int default_value = 0)
        {
            return SaveDataManager.Instance.GetInt(key, default_value);
        }

        public static float GetFloat(string key, float default_value = 0.0f)
        {
            return SaveDataManager.Instance.GetFloat(key, default_value);
        }

        public static bool HasKey(string key)
        {
            return SaveDataManager.Instance.HasKey(key);
        }

        public static void DeleteKey(string key)
        {
            SaveDataManager.Instance.DeleteKey(key);
        }

        #endregion
    }
}
