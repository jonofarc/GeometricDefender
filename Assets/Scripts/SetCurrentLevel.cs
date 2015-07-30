using UnityEngine;
using System.Collections;

public class SetCurrentLevel : MonoBehaviour {
	public int myLevel=1;
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("CurrentLevel", myLevel); 

		if (myLevel + 1 > GlobalVariables.LastLevel) {

			PlayerPrefs.SetInt ("NextLevel",1); 
		} else {

			PlayerPrefs.SetInt ("NextLevel", myLevel + 1); 

		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
