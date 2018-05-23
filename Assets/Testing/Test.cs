using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public bool SpeedTest;
    public bool DeleteAtStart;
	void Start ()
	{

        if (DeleteAtStart)
        {
            FileBasedPrefs.DeleteAll();
        }
        if (SpeedTest)
        {
            for (int i = 0; i < 10; i ++)
            {
                FileBasedPrefs.SetString("SpeedTest" + i, "SpeedTest" + i);
            }
            return;
        }

        Invoke("StringTests", 0.25f);
        Invoke("IntTests", 0.5f);
        Invoke("FloatTests", 0.75f);
        Invoke("BoolTests", 1f);
        Invoke("GeneralTests", 1.25f);

    }

    private void Update()
    {
        if (SpeedTest)
        {
            for (int i = 0; i < 10; i++)
            {
                FileBasedPrefs.SetString("SpeedTest" + i, "SpeedTest" + i);
            }
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            FileBasedPrefs.ManualySave();
        }
    }


    void StringTests()
    {
        FileBasedPrefs.SetString("test", "test");
       
        if(!FileBasedPrefs.GetString("test").Equals("test"))
        {
            Debug.LogException(new System.Exception("SetStringFailed"));
            return;
        }

        FileBasedPrefs.SetString("test", "test2");

        if (!FileBasedPrefs.GetString("test").Equals("test2"))
        {
            Debug.LogException(new System.Exception("ReplaceStringFailed"));
            return;
        }

        if (!FileBasedPrefs.HasKeyForString("test"))
        {
            Debug.LogException(new System.Exception("HasKeyForStringFailed"));
            return;
        }

        FileBasedPrefs.DeleteString("test");

        if (!FileBasedPrefs.GetString("test").Equals(""))
        {
            Debug.LogException(new System.Exception("DeleteStringFailed"));
            return;
        }

        FileBasedPrefs.SetString("test", "test33");

        if (!FileBasedPrefs.GetString("test").Equals("test33"))
        {
            Debug.LogException(new System.Exception("ReplaceStringFailed"));
            return;
        }

        Debug.Log("String Tests Passed");

    }

    void IntTests()
    {
        FileBasedPrefs.SetInt("test", 1);

        if (!FileBasedPrefs.GetInt("test").Equals(1))
        {
            Debug.LogException(new System.Exception("SetIntFailed"));
            return;
        }

        FileBasedPrefs.SetInt("test", 2);

        if (!FileBasedPrefs.GetInt("test").Equals(2))
        {
            Debug.LogException(new System.Exception("ReplaceIntFailed"));
            return;
        }

        if (!FileBasedPrefs.HasKeyForInt("test"))
        {
            Debug.LogException(new System.Exception("HasKeyForIntFailed"));
            return;
        }

        FileBasedPrefs.DeleteInt("test");

        if (!FileBasedPrefs.GetInt("test").Equals(0))
        {
            Debug.LogException(new System.Exception("DeleteIntFailed"));
            return;
        }

        FileBasedPrefs.SetInt("test", 333);

        if (!FileBasedPrefs.GetInt("test").Equals(333))
        {
            Debug.LogException(new System.Exception("ReplaceIntFailed"));
            return;
        }

        Debug.Log("Int Tests Passed");

    }

    void FloatTests()
    {
        FileBasedPrefs.SetFloat("test", 1);

        if (!FileBasedPrefs.GetFloat("test").Equals(1))
        {
            Debug.LogException(new System.Exception("SetFloatFailed"));
            return;
        }

        FileBasedPrefs.SetFloat("test", 2);

        if (!FileBasedPrefs.GetFloat("test").Equals(2))
        {
            Debug.LogException(new System.Exception("ReplaceFloatFailed"));
            return;
        }

        if (!FileBasedPrefs.HasKeyForFloat("test"))
        {
            Debug.LogException(new System.Exception("HasKeyForFloatFailed"));
            return;
        }

        FileBasedPrefs.DeleteFloat("test");

        if (!FileBasedPrefs.GetFloat("test").Equals(0))
        {
            Debug.LogException(new System.Exception("DeleteFloatFailed"));
            return;
        }

        FileBasedPrefs.SetFloat("test", 333.333f);

        if (!FileBasedPrefs.GetFloat("test").Equals(333.333f))
        {
            Debug.LogException(new System.Exception("ReplaceFloatFailed"));
            return;
        }

        Debug.Log("Float Tests Passed");

    }

    void BoolTests()
    {
        FileBasedPrefs.SetBool("test", true);

        if (!FileBasedPrefs.GetBool("test").Equals(true))
        {
            Debug.LogException(new System.Exception("SetBoolFailed"));
            return;
        }

        FileBasedPrefs.SetBool("test", true);

        if (!FileBasedPrefs.GetBool("test").Equals(true))
        {
            Debug.LogException(new System.Exception("ReplaceBoolFailed"));
            return;
        }

        if (!FileBasedPrefs.HasKeyForBool("test"))
        {
            Debug.LogException(new System.Exception("HasKeyForBoolFailed"));
            return;
        }

        FileBasedPrefs.DeleteBool("test");

        if (!FileBasedPrefs.GetBool("test").Equals(false))
        {
            Debug.LogException(new System.Exception("DeleteBoolFailed"));
            return;
        }

        FileBasedPrefs.SetBool("test", true);

        if (!FileBasedPrefs.GetBool("test").Equals(true))
        {
            Debug.LogException(new System.Exception("ReplaceBoolFailed"));
            return;
        }

        Debug.Log("Bool Tests Passed");

    }

    void GeneralTests()
    {
        FileBasedPrefs.SetString("test", "test");

        if (!FileBasedPrefs.HasKey("test"))
        {
            Debug.LogException(new System.Exception("HasKeyFailed"));
            return;
        }

        //FileBasedPrefs.DeleteKey("test");

        //if (FileBasedPrefs.HasKey("test"))
        //{
        //    Debug.LogException(new System.Exception("DeleteKeyFailed"));
        //    return;
        //}

        Debug.Log("General Tests Passed");

    }


}
