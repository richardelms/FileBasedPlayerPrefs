using System.IO;
using UnityEngine;

public static class FileBasedPrefs
{
    private const string SaveFileName = "saveData.txt";

    #region Public Get, Set and Util methods

    public static void SetString(string key, string value)
    {
        AddData(key, value);
    }

    public static string GetString(string key, string defaultValue)
    {
        var saveFile = GetSaveFile();
        return saveFile.GetStringFromKey(key, defaultValue);
    }

    public static void SetInt(string key, int value)
    {
        AddData(key, value);
    }

    public static int GetInt(string key, int defaultValue)
    {
        var saveFile = GetSaveFile();
        return saveFile.GetIntFromKey(key, defaultValue);
    }

    public static void SetFloat(string key, float value)
    {
        AddData(key, value);
    }

    public static float GetFloat(string key, float defaultValue)
    {
        var saveFile = GetSaveFile();
        return saveFile.GetFloatFromKey(key, defaultValue);
    }

    public static void SetBool(string key, bool value)
    {
        AddData(key, value);
    }

    public static bool GetBool(string key, bool defaultValue)
    {
        var saveFile = GetSaveFile();
        return saveFile.GetBoolFromKey(key, defaultValue);
    }

    public static void OverwriteLocalSaveFile(string data)
    {
        WriteToSaveFile(data);
    }

    public static void DeleteAll()
    {
        WriteToSaveFile(JsonUtility.ToJson(new SaveFile()));
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

    #endregion

    #region File Utils

    private static void AddData(string key, object value)
    {
        var saveFile = GetSaveFile();
        saveFile.UpdateOrAddData(key, value);
        SaveSaveFile(saveFile);
    }

    private static void CheckForSaveFile()
    {
        var fileExists = File.Exists(GetSaveFilePath());
        if (!fileExists)
        {
            WriteToSaveFile(JsonUtility.ToJson(new SaveFile()));
        }
    }

    private static void WriteToSaveFile(string data)
    {
        var tw = new StreamWriter(GetSaveFilePath());
        tw.Write(data);
        tw.Close();
    }

    private static void SaveSaveFile(SaveFile data)
    {
        WriteToSaveFile(JsonUtility.ToJson(data));
    }

    private static SaveFile GetSaveFile()
    {
        CheckForSaveFile();
        return JsonUtility.FromJson<SaveFile>(File.ReadAllText(GetSaveFilePath()));
    }

    #endregion
}



