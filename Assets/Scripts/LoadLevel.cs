using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {
	public string LevelToLoad="";
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void loadlevel(){
		Application.LoadLevel (LevelToLoad);
	}
	void OnMouseDown(){
		Invoke ("loadlevel",2);
	}
}
