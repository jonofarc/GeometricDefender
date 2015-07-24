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
		//Application.LoadLevel (LevelToLoad);
		Application.LoadLevel ("GeometricDefenseLvl"+PlayerPrefs.GetInt("CurrentLevel".ToString()));
	}
	void OnMouseDown(){
		loadlevel ();
	}
}
