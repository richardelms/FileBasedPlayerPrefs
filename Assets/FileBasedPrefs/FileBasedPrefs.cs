using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class FileBasedPrefs
{
    private const string SaveFileName = "saveData.txt";

    #region Public Get Set Data methods

    public static void SetString(string key, string value)
    {
        var saveFile = GetSaveFile();
        saveFile.UpdateOrAddData(key, value);
        SaveSaveFile(saveFile);
    }

    public static string GetString(string key, string defaultValue)
    {
        var saveFile = GetSaveFile();
        if (saveFile.HasKey(key))
        {
            return saveFile.GetValueFromKey(key);
        }
        else
        {
            return defaultValue;
        }
    }

    public static void SetInt(string key, int value)
    {
        SetString(key, value.ToString());
    }

    public static int GetInt(string key, int defaultValue)
    {
        return int.Parse(GetString(key, defaultValue.ToString()));
    }

    public static void SetFloat(string key, float value)
    {
        SetString(key, value.ToString());
    }

    public static float GetFloat(string key, float defaultValue)
    {
        return float.Parse(GetString(key, defaultValue.ToString()));
    }

    #endregion

    #region File Utils

    public static string GetSaveFileAsJson()
    {
        CheckForSaveFile();
        return File.ReadAllText(GetSaveFilePath());
    }

    public static void DeleteAll()
    {
        WriteToFile(JsonUtility.ToJson(new SaveFile()));
    }

    public static string GetSaveFilePath()
    {
        return Path.Combine(Application.persistentDataPath, SaveFileName);
    }

    private static void CheckForSaveFile()
    {
        var fileExists = File.Exists(GetSaveFilePath());
        if (!fileExists)
        {
            WriteToFile(JsonUtility.ToJson(new SaveFile()));
        }
    }

    private static void WriteToFile(string data)
    {
        var tw = new StreamWriter(GetSaveFilePath());
        tw.Write(data);
        tw.Close();
    }

    private static void SaveSaveFile(SaveFile data)
    {
        WriteToFile(JsonUtility.ToJson(data));
    }

    private static SaveFile GetSaveFile()
    {
        CheckForSaveFile();
        return JsonUtility.FromJson<SaveFile>(File.ReadAllText(GetSaveFilePath()));
    }

    #endregion

    #region SaveFileDataStructure

    [Serializable]
    public class SaveFile
    {
        public Item[] Data = new Item[0];

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
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i].key.Equals(key))
                {
                    return (Data[i].value);
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
                SetValueForNewKey(key, value);
            }
        }

        private void SetValueForNewKey(string key, string value)
        {
            var dataAsList = Data.ToList();
            dataAsList.Add(new Item(key, value));
            Data = dataAsList.ToArray();
        }

        private void SetValueForExistingKey(string key, string value)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i].key.Equals(key))
                {
                    Data[i].value = value;
                }
            }
        }

        public bool HasKey(string key)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i].key.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }
    }

    #endregion
}



