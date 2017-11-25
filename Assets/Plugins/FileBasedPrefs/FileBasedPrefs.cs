using System.IO;
using UnityEngine;


public static class FileBasedPrefs
{
    private const string SaveFileName = "saveData.txt";
    private const bool ScrambleSaveData = false;
    private const bool AutoSaveData = false;
    private static FileBasedPrefsSaveFileModel _latestData;

    const string String_Empty = "";

    #region Public Get, Set and Util methods

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

    #endregion 

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
        var saveFile = GetSaveFile();
        saveFile.DeleteKey(key);
        SaveSaveFile(saveFile);
    }

    public static void DeleteString(string key)
    {
        var saveFile = GetSaveFile();
        saveFile.DeleteString(key);
        SaveSaveFile(saveFile);
    }

    public static void DeleteInt(string key)
    {
        var saveFile = GetSaveFile();
        saveFile.DeleteInt(key);
        SaveSaveFile(saveFile);
    }

    public static void DeleteFloat(string key)
    {
        var saveFile = GetSaveFile();
        saveFile.DeleteFloat(key);
        SaveSaveFile(saveFile);
    }

    public static void DeleteBool(string key)
    {
        var saveFile = GetSaveFile();
        saveFile.DeleteBool(key);
        SaveSaveFile(saveFile);
    }

    public static void DeleteAll()
    {
        WriteToSaveFile(JsonUtility.ToJson(new FileBasedPrefsSaveFileModel()));
    }

    public static void OverwriteLocalSaveFile(string data)
    {
        WriteToSaveFile(data);
    }

    public static string GetSaveFilePath()
    {
        return Path.Combine(Application.persistentDataPath, SaveFileName);
    }

    public static string GetSaveFileAsJson()
    {
        CheckForSaveFile();
        return File.ReadAllText(GetSaveFilePath());
    }



    #region File Utils

    private static void AddDataToSaveFile(string key, object value)
    {
        var saveFile = GetSaveFile();
        saveFile.UpdateOrAddData(key, value);
        SaveSaveFile(saveFile);
    }

    private static object GetDataFromSaveFile(string key, object defaultValue)
    {
        var saveFile = GetSaveFile();
        return saveFile.GetValueFromKey(key, defaultValue);
    }

    private static void CheckForSaveFile()
    {
        var fileExists = File.Exists(GetSaveFilePath());
        if (!fileExists)
        {
            var blankSaveFile = JsonUtility.ToJson(new FileBasedPrefsSaveFileModel());
            WriteToSaveFile(blankSaveFile);
        }
    }

    public static void ManualySave()
    {
        SaveSaveFile(_latestData,true);
    }

    private static void SaveSaveFile(FileBasedPrefsSaveFileModel data, bool manualSave = false)
    {
        _latestData = data;
        if (AutoSaveData || manualSave)
        {
            WriteToSaveFile(JsonUtility.ToJson(data));
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

    private static FileBasedPrefsSaveFileModel GetSaveFile()
    {        
        if (_latestData == null)
        {
            CheckForSaveFile();
            var saveFileText = File.ReadAllText(GetSaveFilePath());
            if (ScrambleSaveData)
            {
                saveFileText = DataScrambler(saveFileText);
            }
            _latestData = JsonUtility.FromJson<FileBasedPrefsSaveFileModel>(saveFileText);
        }
        return _latestData;
    }

    static string DataScrambler(string data)
    { 
        string codeword = "randomword";
        string res = ""; 
        for (int i = 0; i<data.Length; i++) 
        { 
            res += (char) (data[i] ^ codeword[i % codeword.Length]); 
        } 
        return res; 
    } 

    #endregion
}


