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
		if (PlayerPrefs.GetInt ("CurrentLevel") == -1) {
			Application.LoadLevel ("InfiniteLevel");
		} else {
			Application.LoadLevel ("GeometricDefenseLvl" + PlayerPrefs.GetInt ("CurrentLevel".ToString ()));
		
		}
	}

	void OnMouseDown(){
		loadlevel ();
	}
}
