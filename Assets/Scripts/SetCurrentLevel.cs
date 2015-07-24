using UnityEngine;
using System.Collections;

public class SetCurrentLevel : MonoBehaviour {
	public int myLevel=1;
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("CurrentLevel", myLevel); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
