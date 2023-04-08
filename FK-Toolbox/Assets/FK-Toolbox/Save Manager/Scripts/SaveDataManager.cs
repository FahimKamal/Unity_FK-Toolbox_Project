using System;
using System.IO;
using AYellowpaper.SerializedCollections;
using FullSerializer;
using Popup_Log_System;
using UnityEngine;

namespace SaveManager
{
    /// <summary>
///     This class works same as Unity's default PlayerPrefs class. You can save bool, int,
///     float and string data with a string key. You can also make it run time only. While
///     in run time mode class will temporary save data in ran and after closing the game
///     all data will be lost. Otherwise this class will save data in a json file.
/// </summary>

public class SaveDataManager : MonoBehaviour
{
    [Tooltip("If active Game will save game data while playing. After restarting the game all data will be lost.")]
    [SerializeField]
    private bool runtimeOnly;

    [SerializeField] private PopupEvent popupEvent;

    private string mFileName;


    public GameDataClass Storage { get; private set; }

#if UNITY_EDITOR
    [SerializeField] private SerializedDictionary<string, bool> boolData;
    [SerializeField] private SerializedDictionary<string, int> intData;
    [SerializeField] private SerializedDictionary<string, float> floatData;
    [SerializeField] private SerializedDictionary<string, string> stringData;
#endif


    #region Initialization

    public static SaveDataManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        mFileName = Path.Combine(Application.persistentDataPath, "GameData.json");
        Storage = runtimeOnly ? new GameDataClass() : Load();

#if UNITY_EDITOR
        foreach (var data in Storage.boolData)
        {
            boolData.Add(data.Key, data.Value);
        }
        foreach (var data in Storage.intData)
        {
            intData.Add(data.Key, data.Value);
        }
        foreach (var data in Storage.floatData)
        {
            floatData.Add(data.Key, data.Value);
        }
        foreach (var data in Storage.stringData)
        {
            stringData.Add(data.Key, data.Value);
        }
#endif
        
        DontDestroyOnLoad(gameObject);
    }

    #endregion


    /// <summary>
    ///     Load data from storage.
    /// </summary>
    /// <returns></returns>
    private GameDataClass Load()
    {
        // If the file doesn't exist, the method just returns false. A warning message is written into the Error property.
        if (!FileExists())
        {
            popupEvent.ShowPopup("The file " + mFileName + " doesn't exist.", onlyLog: true);
            return new GameDataClass();
        }

        try
        {
            return LoadJsonFile<GameDataClass>(mFileName);
        }
        catch (Exception e)
        {
            popupEvent.ShowPopup("This system exception has been thrown during loading: " + e.Message,
                onlyLog: true);
            throw;
        }
    }

    /// <summary>
    ///     Save the data into storage.
    /// </summary>
    /// <returns></returns>
    public bool Save(GameDataClass cloudData = null)
    {
        if (runtimeOnly)
        {
            popupEvent.ShowPopup("Runtime mode active, not saving to disk.");

            if (cloudData == null) return true;

            Storage = cloudData;
            popupEvent.ShowPopup("Game data from cloud is stored in RAM.");
            return true;
        }

        try
        {
            if (cloudData != null)
            {
                SaveJsonFile(mFileName, cloudData);
                popupEvent.ShowPopup("Game data from cloud is stored in disk.");
                return true;
            }

            SaveJsonFile(mFileName, Storage);
            return true;
        }
        catch (Exception e)
        {
            popupEvent.ShowPopup("This system exception has been thrown during saving: " + e.Message,
                onlyLog: true);
            return false;
        }
    }



    #region Check methods

    /// <summary>
    ///     Return TRUE if the file exists.
    /// </summary>
    /// <returns></returns>
    private bool FileExists()
    {
        return File.Exists(mFileName);
    }

    /// <summary>
    ///     Checks if a key exists in the storage or not.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool HasKey(string key, DataType type = DataType.All)
    {
        if (Storage == null) return false;
        switch (type)
        {
            case DataType.Boolean:
                return Storage.boolData.ContainsKey(key);
            case DataType.Integer:
                return Storage.intData.ContainsKey(key);
            case DataType.Float:
                return Storage.floatData.ContainsKey(key);
            case DataType.String:
                return Storage.stringData.ContainsKey(key);
            case DataType.All:
                return Storage.boolData.ContainsKey(key) || Storage.intData.ContainsKey(key) ||
                       Storage.floatData.ContainsKey(key) || Storage.stringData.ContainsKey(key);
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public void DeleteKey(string key)
    {
        if (HasKey(key, DataType.Boolean))
        {
            Storage.boolData.Remove(key);
#if UNITY_EDITOR
            boolData.Remove(key);
#endif
        }
        else if (HasKey(key, DataType.Float))
        {
            Storage.floatData.Remove(key);
#if UNITY_EDITOR
            floatData.Remove(key);
#endif
        }
        else if (HasKey(key, DataType.Integer))
        {
            Storage.intData.Remove(key);
#if UNITY_EDITOR
            intData.Remove(key);
#endif
        }
        else if (HasKey(key, DataType.String))
        {
            Storage.stringData.Remove(key);
#if UNITY_EDITOR
            stringData.Remove(key);
#endif
        }
    }

    #endregion

    #region Set/Get data from Storage

    /// <summary>
    ///     Adds Boolean data in storage
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetBool(string key, bool value)
    {
        //var storage = Load();
        if (HasKey(key, DataType.Boolean))
        {
            Storage.boolData[key] = value;
#if UNITY_EDITOR
            if (boolData.ContainsKey(key))
            {
                boolData.Remove(key);
                boolData.Add(key, value);
            }
#endif
        }
        else
        {
            Storage.boolData.Add(key, value);
#if UNITY_EDITOR
            boolData.Add(key, value);
#endif
        }

        Save();
    }

    /// <summary>
    ///     Adds Integer data in storage
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetInt(string key, int value)
    {
        //var storage = Load();
        if (HasKey(key, DataType.Integer))
        {
            Storage.intData[key] = value;
#if UNITY_EDITOR
            if (intData.ContainsKey(key))
            {
                intData.Remove(key);
                intData.Add(key, value);
            }
#endif
        }
        else
        {
            Storage.intData.Add(key, value);
#if UNITY_EDITOR
            intData.Add(key, value);
#endif
        }
        
        Save();
    }

    /// <summary>
    ///     Adds Float data in storage
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetFloat(string key, float value)
    {
        //var storage = Load();
        if (HasKey(key, DataType.Float))
        {
            Storage.floatData[key] = value;
#if UNITY_EDITOR
            if (floatData.ContainsKey(key))
            {
                floatData.Remove(key);
                floatData.Add(key, value);
            }
#endif
        }
        else
        {
            Storage.floatData.Add(key, value);
#if UNITY_EDITOR
            floatData.Add(key, value);
#endif
        }

        Save();
    }

    /// <summary>
    ///     Adds Integer data in storage
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetString(string key, string value)
    {
        //var storage = Load();
        if (HasKey(key, DataType.String))
        {
            Storage.stringData[key] = value;
#if UNITY_EDITOR
            if (stringData.ContainsKey(key))
            {
                stringData.Remove(key);
                stringData.Add(key, value);
            }
#endif
        }
        else
        {
            Storage.stringData.Add(key, value);
#if UNITY_EDITOR
            stringData.Add(key, value);
#endif
        }

        Save();
    }

    /// <summary>
    ///     Return the boolean data for the given key (or the defined defaultValue if nothing found).
    /// </summary>
    /// <param name="key">The key to search in storage.</param>
    /// <param name="defaultValue">If key doesn't exists, return this default value.</param>
    /// <returns>Value from storage.</returns>
    public bool GetBool(string key, bool defaultValue = false)
    {
        //var storage = Load();
        try
        {
            return HasKey(key, DataType.Boolean) ? Storage.boolData[key] : defaultValue;
        }
        catch (Exception)
        {
            popupEvent.ShowPopup("GetBool error using key: " + key);
            return defaultValue;
        }
    }

    /// <summary>
    ///     Return the integer data for the given key (or the defined defaultValue if nothing found).
    /// </summary>
    /// <param name="key">The key to search in storage.</param>
    /// <param name="defaultValue">If key doesn't exists, return this default value.</param>
    /// <returns>Value from storage.</returns>
    public int GetInt(string key, int defaultValue = 0)
    {
        //var storage = Load();
        try
        {
            return HasKey(key, DataType.Integer) ? Storage.intData[key] : defaultValue;
        }
        catch (Exception)
        {
            popupEvent.ShowPopup("GetInt error using key: " + key);
            return defaultValue;
        }
    }

    /// <summary>
    ///     Return the float data for the given key (or the defined defaultValue if nothing found).
    /// </summary>
    /// <param name="key">The key to search in storage.</param>
    /// <param name="defaultValue">If key doesn't exists, return this default value.</param>
    /// <returns>Value from storage.</returns>
    public float GetFloat(string key, float defaultValue = 0f)
    {
        //var storage = Load();
        try
        {
            return HasKey(key, DataType.Float) ? Storage.floatData[key] : defaultValue;
        }
        catch (Exception)
        {
            popupEvent.ShowPopup("GetFloat error using key: " + key);
            return defaultValue;
        }
    }

    /// <summary>
    ///     Return the string data for the given key (or the defined defaultValue if nothing found).
    /// </summary>
    /// <param name="key">The key to search in storage.</param>
    /// <param name="defaultValue">If key doesn't exists, return this default value.</param>
    /// <returns>Value from storage.</returns>
    public string GetString(string key, string defaultValue = "")
    {
        //var storage = Load();
        try
        {
            return HasKey(key, DataType.String) ? Storage.stringData[key] : defaultValue;
        }
        catch (Exception)
        {
            popupEvent.ShowPopup("GetString error using key: " + key);
            return defaultValue;
        }
    }

    public void DeleteAll()
    {
        Storage = new GameDataClass();
#if UNITY_EDITOR
        boolData = new SerializedDictionary<string, bool>();
        intData = new SerializedDictionary<string, int>();
        floatData = new SerializedDictionary<string, float>();
        stringData = new SerializedDictionary<string, string>();
#endif
        Save();
    }

    #endregion

    #region Read/Write data to disk

    /// <summary>
    ///     Saves the specified data to a json file at the specified path.
    /// </summary>
    /// <param name="path">The path to the json file.</param>
    /// <param name="data">The data to save.</param>
    /// <typeparam name="T">The type of the data to serialize to the file.</typeparam>
    private static void SaveJsonFile<T>(string path, T data) where T : class
    {
        fsData serializedData;
        var serializer = new fsSerializer();
        serializer.TrySerialize(data, out serializedData).AssertSuccessWithoutWarnings();
        var file = new StreamWriter(path);
        var json = fsJsonPrinter.PrettyJson(serializedData);
        file.WriteLine(json);
        file.Close();
    }

    /// <summary>
    ///     Loads the json file at the specified path.
    /// </summary>
    /// <param name="path">The path to the json file.</param>
    /// <typeparam name="T">The type of the data to which to deserialize the file to.</typeparam>
    /// <returns></returns>
    private T LoadJsonFile<T>(string path) where T : class
    {
        if (!File.Exists(path))
        {
            popupEvent.ShowPopup("File not found at path: " + path);
            popupEvent.ShowPopup("Creating new file at path: " + path);
        }

        var file = new StreamReader(path);
        var fileContents = file.ReadToEnd();
        var data = fsJsonParser.Parse(fileContents);
        object deserialized = null;
        var serializer = new fsSerializer();
        serializer.TryDeserialize(data, typeof(T), ref deserialized).AssertSuccessWithoutWarnings();
        file.Close();

        Debug.Log("data is loaded." + deserialized as T);
        return deserialized as T;
    }

    #endregion
}
}