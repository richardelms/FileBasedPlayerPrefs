# FBPP (File Based Player Prefs)

### Overview

This Unity library is designed to replace the standard Unity class, PlayerPrefs. 

The main difference being that it saves all records in JSON format to a text file.

This makes it much easier to store your save game data in places like the Steam Cloud services, iCloud, Google Play or your own backend etc..

The Library also adds some functionality such as extra record types, encryption and more refined/flexible data access.

Keys are type specific, meaning that if you save a string under the key "key1" it will not be overridden when you save an int with the same key "key1";

#### Platform Support

Tested on Windows, OSX, Linux, iOS and Android.

Supports any Unity version that can load UPM packages. This generally means 2017.1+

## Installation

Install it via `Unity Package Manager`:
```
https://github.com/richardelms/FileBasedPlayerPrefs.git
```
## Initialisation

### With Default settings "Not Recommended!"

If you do not wish to use any custom options you can just call any of the FPBB methods without worrying about initialisation.

By default, FBPP will:

- Save your data every time a value is set
- Encrypt your data every time a value is set
- Save your data in the Application.persistentDataPath directory
- Name the file "saveData.txt"

### With Custom settings "Recommended!"

If you wish to customise the behaviour of FBPP you should use the following method before calling any other FBPP methods:

```
// enter your custom settings
var config = new FBPPConfig()
{
    SaveFileName = "my-save-file.txt",
    AutoSaveData = false,
    ScrambleSaveData = false,
    EncryptionSecret = "my-secret",
    SaveFilePath = "my/explicit/savefile/path"
};
// pass them to FBPP
FBPP.Start(config);
```


## Save File Encryption

I have added a very simple scrambler to the saved json so that players cannot easily cheat by changing the values in the saved game file, this can be enabled or disabled by initialising with custom settings. Please note, this is not super secure encryption, just a small string scrambler method that i think will stop 99.99% of users from editing the data.

If you choose a custom value for the Encryption secret and release a version of your software with it, you must stick with it or it will break all old save files that were created with the old value.

## Usage

### Get and Set
```
    FBPP.SetString(string key, string value);

    FBPP.GetString(string key, (optional)string defaultValue);
    
    FBPP.SetInt(string key, int value);

    FBPP.GetInt(string key, (optional)int defaultValue); 
    
    FBPP.SetFloat(string key, float value);

    FBPP.GetFloat(string key, (optional)float defaultValue); 
    
    FBPP.SetBool(string key, bool value);

    FBPP.GetBool(string key, (optional)bool defaultValue);
    
```
### Util Methods
```
    FBPP.HasKey(String key); will return true if there is any data type saved under the requested key
    
    FBPP.HasKeyForString(String key); will return true if there is a string saved under the requested key
    
    FBPP.HasKeyForInt(String key); will return true if there is a int saved under the requested key
    
    FBPP.HasKeyForFloat(String key); will return true if there is a float saved under the requested key
    
    FBPP.HasKeyForBool(String key); will return true if there is a bool saved under the requested key
    
    FBPP.DeleteKey(String key); This will delete ALL data records saved under the key regardless of type
    
    FBPP.DeleteString(String key); This will delete any string recorded under the key
    
    FBPP.DeleteInt(String key); This will delete any int recorded under the key
    
    FBPP.DeleteFloat(String key); This will delete any float recorded under the key
    
    FBPP.DeleteBool(String key); This will delete any bool recorded under the key

    FBPP.DeleteAll(); // Deletes all records and replaces the save file with a blank one
    
```
### Save File Helper Methods
```
    FBPP.ManualySave(); // see the Advanced Usage section for important details regarding optimisation. 

    FBPP.GetSaveFileAsJson(); // returns the saved prefs as a json object in string format.
    
    FBPP.GetSaveFilePath(); // returns the full path to your save file.
    
    FBPP.OverwriteLocalSaveFile(string data); // overwrites the save file with whatever data you like. Warning,       this will break the FBPP methods if the data you save is not in the SaveFile json format.
```

## Advanced Usage / Speed optimisation / Manual File writing

Unfortunately, encrypting a text file is pretty slow, no matter how you do it, so if you are using one of the Set methods during active gameplay, you might notice some slowdown in fps.

To counteract this i have included some advanced features that give you more control of when and how the library saves/encrypts data.

If the config value AutoSaveData is set to true (the default setting), then every time a Set method is called, it will write and encrypt the save data immediately.

If set to false, then it will only save and encrypt the data to file when you call FBPP();

This means that the save data is stored in memory until you specifically tell it to write the file. This means you can choose a better time to save all the data and not take any noticeable performance hits.
