using System.IO;
using UnityEngine;
using System;


public static class FileBasedPrefs
{
    private const string SaveFileName = "saveData.txt";
    private const bool ScrambleSaveData = true;
    private const string EncryptionCodeword = "CHANGE ME TO YOUR OWN RANDOM STRING";
    private const bool AutoSaveData = true;
    private static FileBasedPrefsSaveFileModel _latestData;

    const string String_Empty = "";

    #region Public Get, Set and util

    public static void SetString(string key, string value = String_Empty)
    {
        AddDataToSaveFile(key, value);
    }

    public static string GetString(string key, string defaultValue = String_Empty)
    {
        return (string) GetDataFromSaveFile(key, defaultValue);
    }

    public static void SetInt(string key, int value = default(int))
    {
        AddDataToSaveFile(key, value);
    }

    public static int GetInt(string key, int defaultValue = default(int))
    {
        return (int) GetDataFromSaveFile(key, defaultValue);
    }

    public static void SetFloat(string key, float value = default(float))
    {
        AddDataToSaveFile(key, value);
    }

    public static float GetFloat(string key, float defaultValue = default(float))
    {
        return (float) GetDataFromSaveFile(key, defaultValue);
    }

    public static void SetBool(string key, bool value = default(bool))
    {
        AddDataToSaveFile(key, value);
    }

    public static bool GetBool(string key, bool defaultValue = default(bool))
    {
        return (bool) GetDataFromSaveFile(key, defaultValue);
    }

    public static bool HasKey(string key)
    {
        return GetSaveFile().HasKey(key);
    }

    public static bool HasKeyForString(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, string.Empty);
    }

    public static bool HasKeyForInt(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, default(int));
    }

    public static bool HasKeyForFloat(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, default(float));
    }

    public static bool HasKeyForBool(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, default(bool));
    }

    public static void DeleteKey(string key)
    {
        GetSaveFile().DeleteKey(key);
        SaveSaveFile();
    }

    public static void DeleteString(string key)
    {
        GetSaveFile().DeleteString(key);
        SaveSaveFile();
    }

    public static void DeleteInt(string key)
    {
        GetSaveFile().DeleteInt(key);
        SaveSaveFile();
    }

    public static void DeleteFloat(string key)
    {
        GetSaveFile().DeleteFloat(key);
        SaveSaveFile();
    }

    public static void DeleteBool(string key)
    {
        GetSaveFile().DeleteBool(key);
        SaveSaveFile();
    }

    public static void DeleteAll()
    {
        WriteToSaveFile(JsonUtility.ToJson(new FileBasedPrefsSaveFileModel()));
        _latestData = new FileBasedPrefsSaveFileModel();
    }

    public static void OverwriteLocalSaveFile(string data)
    {
        WriteToSaveFile(data);
        _latestData = null;
    }


    #endregion



    #region Read data

    private static FileBasedPrefsSaveFileModel GetSaveFile()
    {
        CheckSaveFileExists();
        if (_latestData == null)
        {
            var saveFileText = File.ReadAllText(GetSaveFilePath());
            if (ScrambleSaveData)
            {
                saveFileText = DataScrambler(saveFileText);
            }
            try
            {
                _latestData = JsonUtility.FromJson<FileBasedPrefsSaveFileModel>(saveFileText);
            }
            catch(ArgumentException e)
            {
                Debug.LogException(new Exception("SAVE FILE IN WRONG FORMAT, CREATING NEW SAVE FILE : " + e.Message));
                DeleteAll();
            }
        }
        return _latestData;
    }

    public static string GetSaveFilePath()
    {
        return Path.Combine(Application.persistentDataPath, SaveFileName);
    }

    public static string GetSaveFileAsJson()
    {
        CheckSaveFileExists();
        return File.ReadAllText(GetSaveFilePath());
    }

    private static object GetDataFromSaveFile(string key, object defaultValue)
    {
        return GetSaveFile().GetValueFromKey(key, defaultValue);
    }

    #endregion


    #region write data

    private static void AddDataToSaveFile(string key, object value)
    {
        GetSaveFile().UpdateOrAddData(key, value);
        SaveSaveFile();
    }
    public static void ManualySave()
    {
        SaveSaveFile(true);
    }

    private static void SaveSaveFile(bool manualSave = false)
    {
        if (AutoSaveData || manualSave)
        {
            WriteToSaveFile(JsonUtility.ToJson(GetSaveFile()));
        }
    }
    private static void WriteToSaveFile(string data)
    {
        var tw = new StreamWriter(GetSaveFilePath());
        if (ScrambleSaveData)
        {
            data = DataScrambler(data);
        }
        tw.Write(data);
        tw.Close();
    }

    #endregion


    #region File Utils

    private static void CheckSaveFileExists()
    {
        if (!DoesSaveFileExist())
        {
            CreateNewSaveFile();
        }
    }

    private static bool DoesSaveFileExist()
    {
        return File.Exists(GetSaveFilePath());
    }

    private static void CreateNewSaveFile()
    {
        WriteToSaveFile(JsonUtility.ToJson(new FileBasedPrefsSaveFileModel()));
    }

    static string DataScrambler(string data)
    { 
        string res = ""; 
        for (int i = 0; i<data.Length; i++) 
        { 
            res += (char) (data[i] ^ EncryptionCodeword[i % EncryptionCodeword.Length]); 
        } 
        return res; 
    } 

    #endregion
}


