using UnityEngine;
using System.Collections;

public class SetCurrentLevel : MonoBehaviour
{
	public int myLevel = 1;
	// Use this for initialization
	void Start ()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		PlayerPrefs.SetInt ("CurrentLevel", myLevel); 
		PlayerPrefs.SetInt ("NextLevel", myLevel + 1); 

	}

	void Awake ()
	{
       
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}
