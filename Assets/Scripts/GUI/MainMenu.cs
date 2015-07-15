using UnityEngine;
using System.Collections;


public class MainMenu : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void LoadLevel(string LevelToLoad){
	
		Application.LoadLevel (LevelToLoad);
	}
	public void Exit(){
		
		Application.Quit ();
	}
	void OnGUI(){
		//if (GUI.Button (new Rect (10, 70, 50, 30), "Click")) {
		//	Debug.Log("Clicked the button with text");
		//}
			
	}// end of GUI
}
