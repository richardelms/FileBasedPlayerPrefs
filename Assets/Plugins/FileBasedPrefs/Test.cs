using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
        FileBasedPrefs.SetString("Test","ddd");
        FileBasedPrefs.SetInt("Test", 333);
        FileBasedPrefs.SetFloat("Test", 333.1f);
        FileBasedPrefs.SetBool("Test", true);
        Invoke("Result", 0.5f);
    }

    void Result()
    {
        Debug.Log(FileBasedPrefs.GetString("Test","Failed"));
        Debug.Log(FileBasedPrefs.GetInt("Test", 0));
        Debug.Log(FileBasedPrefs.GetFloat("Test", 0));
        Debug.Log(FileBasedPrefs.GetBool("Test", false));
    }


}
