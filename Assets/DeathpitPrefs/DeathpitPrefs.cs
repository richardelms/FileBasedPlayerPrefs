using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using System.IO;
using UnityEngine.UI;

public class DeathpitPrefs : MonoBehaviour
{

    public Text DebugText;

    void Log(string value)
    {
        DebugText.text += "\n" + value;
    }

    string SavedDataFilePath;

    private void Awake()
    {
        SavedDataFilePath = string.Format("C:/Users/richa/AppData/LocalLow/DefaultCompany/deathpitPrefs\\dpSave.txt");
        //SavedDataFilePath = Path.Combine(Application.persistentDataPath, "dpSave.txt");
        Log(SavedDataFilePath);
    }

    private void Start()
    {
        StartCoroutine("Test");
    }

    IEnumerator Test()
    {
        //SetString("Test","Test");
        yield return new WaitForSeconds(1);
        Log(GetString("Test","Not Found"));
    }


    void SetString(string key, string value)
    {
        var saveFile = GetSaveFile();
        saveFile.UpdateOrAddData(key,value);
        SaveSaveFile(saveFile);
    }

    string GetString(string key, string defaultValue)
    {
        var saveFile = GetSaveFile();
        if(saveFile.HasKey(key))
        {
            return saveFile.GetValueFromKey(key);
        }
        else
        {
            return defaultValue;
        }
    }

    void CheckForSaveFile()
    {
        var fileExists = File.Exists(SavedDataFilePath);
        if (!fileExists)
        {
            WriteToFile(JsonUtility.ToJson(new SaveFile()));
        }
    }


    void WriteToFile(string data)
    {
        var tw = new StreamWriter(SavedDataFilePath);
        tw.Write(data);
        tw.Close();
    }


    void SaveSaveFile(SaveFile data)
    {
        WriteToFile(JsonUtility.ToJson(data));
    }

    SaveFile GetSaveFile()
    {
        CheckForSaveFile();
        return JsonUtility.FromJson<SaveFile>(File.ReadAllText(SavedDataFilePath));
    }

}

[Serializable]
public class SaveFile{
    
    public Item[] data = new Item[0];

    [Serializable]
    public class Item
    {
        public string key;
        public string value;

        public Item(string K, string V)
        {
            key = K;
            value = V;
        }
    }

    public string GetValueFromKey(string key)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if(data[i].key.Equals(key))
            {
                return (data[i].value);
            }
        }
        return string.Empty;
    }

    public void UpdateOrAddData(string key, string value)
    {
        if (HasKey(key))
        {
            SetValueForExistingKey(key, value);
        }
        else
        {
            SetValueForNewKey(key,value);
        }
    }

    void SetValueForNewKey(string key, string value)
    {
        var dataAsList = data.ToList();
        dataAsList.Add(new Item(key,value));
        data = dataAsList.ToArray();
    }

    void SetValueForExistingKey(string key, string value)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if(data[i].key.Equals(key))
            {
                data[i].value = value;
            }
        }
    }

    public bool HasKey(string key)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i].key.Equals(key))
            {
                return true;
            }
        }
        return false;
    }
}



