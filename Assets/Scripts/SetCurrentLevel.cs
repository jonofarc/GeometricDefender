using UnityEngine;
using System.Collections;

public class SetCurrentLevel : MonoBehaviour {
	public int myLevel=1;
	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		PlayerPrefs.SetInt ("CurrentLevel", myLevel); 
		PlayerPrefs.SetInt ("NextLevel", myLevel + 1); 

		
		Debug.Log (PlayerPrefs.GetInt("NextLevel").ToString());

	}
    void Awake()
    {
        Application.targetFrameRate = 38;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
