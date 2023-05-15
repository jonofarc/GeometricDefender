using UnityEngine;

public class HighScoreReset : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.SetInt(Application.loadedLevelName + "HP", 0);
        PlayerPrefs.SetInt(Application.loadedLevelName + "Wave", 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
