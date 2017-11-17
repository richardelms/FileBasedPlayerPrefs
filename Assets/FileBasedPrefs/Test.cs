using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    Debug.Log(FileBasedPrefs.GetSaveFileAsJson());
	    Debug.Log(FileBasedPrefs.GetSaveFilePath());
    }


}
