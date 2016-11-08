using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {
	public string LevelToLoad="";
	public bool Autostart=false;
	public float time=2f;

	// Use this for initialization
	void Start () {
		if(Autostart){
			Invoke ("AutoLoadLevel",time);
		}

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
	void AutoLoadLevel(){
        if (LevelToLoad == "")
        {
            Application.LoadLevel("MainMenu");
        }
        else {
            Application.LoadLevel(LevelToLoad);
        }
		
	
	}
}
