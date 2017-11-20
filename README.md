# JSON File Based PlayerPrefs

This Unity library is designed to replace the standard Unity PlayerPrefs. 

The main difference being that it saves all records in JSON format to a file in the Application.persistentDataPath.

This makes it much easier to save your save game data in places like the Steam Cloud services, iCloud, Google Play or your own backend etc..

The Library also adds some functionality such as extra record types and more refined/flexible data access.

## NOTES:

Keys are type specific, meaning that if you save a string under the key "key1" it will not be overridden when you save an int with the same key "key1";

Tested on Windows, OSX and Android.

## Installation

Grab the latest unity package from the releases tab of this repo and import it into your project.

#### Save File Name

At the top of the script FileBasedPrefs.cs you can specify what name you would like your save file to have in the string SaveFileName.

#### Save File Encryption

I have added a very simple scrambler to the saved json so that players cannot easily cheat by changing the values in the saved game file, this can be enabled or disabled with the bool ScrambleSaveData at the top of FileBasedPrefs.cs. Please note, this is not super secure encription, just a small string scrambler method that i think will stop 99.99% of users from editing the data.

## Usage

You must include the STF.FileBasedPrefs namespace at the top of any script you use this in:
```
using STF.FileBasedPrefs;
```
### Get and Set
```
    FileBasedPrefs.SetString(string key, string value);

    FileBasedPrefs.GetString(string key, (optional)string defaultValue);
    
    FileBasedPrefs.SetInt(string key, int value);

    FileBasedPrefs.GetInt(string key, (optional)int defaultValue); 
    
    FileBasedPrefs.SetFloat(string key, float value);

    FileBasedPrefs.GetFloat(string key, (optional)float defaultValue); 
    
    FileBasedPrefs.SetBool(string key, bool value);

    FileBasedPrefs.GetBool(string key, (optional)bool defaultValue);
    
```
### Util Methods
```
    FileBasedPrefs.HasKey(String key); will return true if there is any data type saved under the requested key
    
    FileBasedPrefs.HasKeyForString(String key); will return true if there is a string saved under the requested key
    
    FileBasedPrefs.HasKeyForInt(String key); will return true if there is a int saved under the requested key
    
    FileBasedPrefs.HasKeyForFloat(String key); will return true if there is a float saved under the requested key
    
    FileBasedPrefs.HasKeyForBool(String key); will return true if there is a bool saved under the requested key
    
    FileBasedPrefs.DeleteKey(String key); This will delete ALL data records saved under the key regardless of type
    
    FileBasedPrefs.DeleteString(String key); This will delete any string recorded under the key
    
    FileBasedPrefs.DeleteInt(String key); This will delete any int recorded under the key
    
    FileBasedPrefs.DeleteFloat(String key); This will delete any float recorded under the key
    
    FileBasedPrefs.DeleteBool(String key); This will delete any bool recorded under the key

    FileBasedPrefs.DeleteAll(); // Deletes all records and replaces the save file with a blank one
    
```
### Save File Helper Methods
```
    FileBasedPrefs.GetSaveFileAsJson(); // returns the saved prefs as a json object in string format 
    
    FileBasedPrefs.GetSaveFilePath(); // returns the full path to your save file.
    
    FileBasedPrefs.OverwriteLocalSaveFile(string data); // overwrites the save file with whatever data you like. Warning,       this will break the FileBasedPrefs methods if the data you save is not in the SaveFile json format
```
