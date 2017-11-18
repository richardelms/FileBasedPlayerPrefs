using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
        FileBasedPrefs.SetString("Test","valueeee");
        FileBasedPrefs.SetInt("Test", 555);
        FileBasedPrefs.SetFloat("Float", 555.444f);
        Invoke("Result",1);
    }

    void Result()
    {
        Debug.Log(FileBasedPrefs.GetString("Test","Failed"));
        Debug.Log(FileBasedPrefs.GetInt("Test", 0));
        Debug.Log(FileBasedPrefs.GetFloat("Test", 0));
    }

}
