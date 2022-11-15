using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SavingSystem 
{
    public static void Save(GameData gameData)
    {
        string path = GetPath();
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(path, FileMode.Create);
        formatter.Serialize(fs, gameData);
        fs.Close();
    }
    public static GameData Load()
    {
        if(!File.Exists(GetPath()))
        {
            GameData emptyData = new GameData();
            Save(emptyData);
            return emptyData;
        }
        string path = GetPath();
        Debug.Log(path);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(path, FileMode.Open);
        GameData data = formatter.Deserialize(fs) as GameData;
        fs.Close();
        return data;
    }
    private static string GetPath()
    {
        return Application.persistentDataPath + "/save.sav";
    }
    
}
