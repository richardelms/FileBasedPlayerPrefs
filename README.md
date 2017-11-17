# FileBasedPlayerPrefs

This small library is designed to act exactly like Unity PlayerPrefs, but saves all prefs in JSON format to a file in the Application.persistentDataPath.

This makes it easier to send or recive saved data with services like Unity Cloud Sync or your own backend services;

NOTES:

Only tested on Windows and OSX standalone, might work on others but didnt get the time to test yet.

## TODO

Add Suffix support for multiple save files at a time.

Make keys type exclusive

Add more types (bool, array etc).

## Example Usage

### Get and Set
```
    FileBasedPrefs.SetString("TestString","test");

    FileBasedPrefs.GetString("TestString",string.Empty); // returns "test"
    
    FileBasedPrefs.SetInt("TestInt",333);

    FileBasedPrefs.GetInt("TestInt",0); // returns 333
    
    FileBasedPrefs.SetFloat("TestFloat",123.123f);

    FileBasedPrefs.GetFloat("TestFloat",0); // returns 123.123f
```
### File Helper Methods
```
    FileBasedPrefs.GetSaveFileAsJson(); // returns the saved prefs as a json object in string format 
    
    FileBasedPrefs.GetSaveFilePath(); // returns the full path to your save file. tested on windows and OSX
    
    FileBasedPrefs.DeleteAll(); // Deletes all records and replaces the save file with a blank one
```
