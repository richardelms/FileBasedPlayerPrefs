using UnityEngine;

public class FileBasedPrefsQuitListner : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationQuit()
    {
        FileBasedPrefs.ManualySave();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        FileBasedPrefs.ManualySave();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        FileBasedPrefs.ManualySave();
    }


}
