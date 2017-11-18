# FileBasedPlayerPrefs

This small Unity library is designed to act exactly like Unity PlayerPrefs, but saves all prefs in JSON format to a file in the Application.persistentDataPath.

This makes it easier to save your save game data in places like the Steam Cloud or your own backend etc..

Avalable types: string, int, float and bool.

## NOTES:

Keys are type specific, meaning that if you save a string under the key "key1" it will not be overridden when you save an int with the same key "key1";

Only tested on Windows and OSX standalone, might work on others but didnt get the time to test yet.

## Installation

Grab the latest unity package from the releases tab of this repo and import it into your project.

## Configuration

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
    FileBasedPrefs.SetString("TestString","test");

    FileBasedPrefs.GetString("TestString",string.Empty); // returns "test"
    
    FileBasedPrefs.SetInt("TestInt",333);

    FileBasedPrefs.GetInt("TestInt",0); // returns 333
    
    FileBasedPrefs.SetFloat("TestFloat",123.123f);

    FileBasedPrefs.GetFloat("TestFloat",0); // returns 123.123f
    
    FileBasedPrefs.SetBool("TestBool",true);

    FileBasedPrefs.GetBool("TestBool",false); // returns true
```
### File Helper Methods
```
    FileBasedPrefs.GetSaveFileAsJson(); // returns the saved prefs as a json object in string format 
    
    FileBasedPrefs.GetSaveFilePath(); // returns the full path to your save file. tested on windows and OSX
    
    FileBasedPrefs.DeleteAll(); // Deletes all records and replaces the save file with a blank one
    
    FileBasedPrefs.OverwriteLocalSaveFile(string data); // overwrites the save file with whatever data you like.
```
