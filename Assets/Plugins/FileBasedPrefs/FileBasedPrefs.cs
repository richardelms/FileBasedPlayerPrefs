using System.IO;
using UnityEngine;
namespace STF.FileBasedPrefs
{
    public static class FileBasedPrefs
    {
        private const string SaveFileName = "saveData.txt";
        private const bool ScrambleSaveData = true;

        #region Public Get, Set and Util methods

        public static void SetString(string key, string value = default(string))
        {
            AddDataToSaveFile(key, value);
        }

        public static string GetString(string key, string defaultValue = default(string))
        {
            return (string)GetDataFromSaveFile(key, defaultValue);
        }

        public static void SetInt(string key, int value = default(int))
        {
            AddDataToSaveFile(key, value);
        }

        public static int GetInt(string key, int defaultValue = default(int))
        {
            return (int)GetDataFromSaveFile(key, defaultValue);
        }

        public static void SetFloat(string key, float value = default(float))
        {
            AddDataToSaveFile(key, value);
        }

        public static float GetFloat(string key, float defaultValue = default(float))
        {
            return (float)GetDataFromSaveFile(key, defaultValue);
        }

        public static void SetBool(string key, bool value = default(bool))
        {
            AddDataToSaveFile(key, value);
        }

        public static bool GetBool(string key, bool defaultValue = default(bool))
        {
            return (bool)GetDataFromSaveFile(key, defaultValue);
        }

        public static bool HasKey(string key)
        {
            return GetSaveFile().HasKey(key);
        }

        public static void DeleteKey(string key)
        {
            var saveFile = GetSaveFile();
            saveFile.DeleteKey(key);
            SaveSaveFile(saveFile);
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
                var blankSaveFile = JsonUtility.ToJson(new SaveFile());
                WriteToSaveFile(blankSaveFile);
            }
        }

        private static void WriteToSaveFile(string data)
        {
            var tw = new StreamWriter(GetSaveFilePath());
            if(ScrambleSaveData)
            {
                data = JsonScrambler(data);
            }
            tw.Write(data);
            tw.Close();
        }

        static string JsonScrambler(string data)
        {
            string codeword = "fjnskabasflbdcj";
            string res = "";
            for (int i = 0; i < data.Length; i++)
            {
                res += (char)(data[i] ^ codeword[i % codeword.Length]);
            }
            return res;
        }

        private static void SaveSaveFile(SaveFile data)
        {
            WriteToSaveFile(JsonUtility.ToJson(data));
        }

        private static SaveFile GetSaveFile()
        {
            CheckForSaveFile();
            var saveFileText = File.ReadAllText(GetSaveFilePath());
            if (ScrambleSaveData)
            {
                saveFileText = JsonScrambler(saveFileText);
            }
            return JsonUtility.FromJson<SaveFile>(saveFileText);
        }

        #endregion
    }

}

