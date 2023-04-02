using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

public enum DataType
{
    Boolean,
    Integer,
    Float,
    String,
    All
}

[Serializable]
public class GameDataClass
{
    [SerializedDictionary("somethiong", "Value")]
    public SerializedDictionary<string, bool> boolData;
    public Dictionary<string, float> floatData;
    public Dictionary<string, int> intData;
    public Dictionary<string, string> stringData;

    public GameDataClass()
    {
        boolData = new SerializedDictionary<string, bool>();
        intData = new Dictionary<string, int>();
        floatData = new Dictionary<string, float>();
        stringData = new Dictionary<string, string>();
    }
}