using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {

	public GameObject ContinueButton;

	public Text NewGameText;
	public Text ContinueText;
	public Text LanguageText;
	public Text ExitText;

	// Use this for initialization
	void Start () {
		//Debug.Log (_language); 
		//language section //we make sure that the language is a valid one and that its on the latest language used
	

		// show continue button only if current level is more than the second one
		if (PlayerPrefs.GetInt ("CurrentLevel") > 1) {
			ContinueButton.SetActive (true);
		} else {
			ContinueButton.SetActive (false);
		}
		//change text of objects to current language

		RefreshTexts ();

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Continue(){
		
		Application.LoadLevel ("GeometricDefenseLvl"+PlayerPrefs.GetInt("CurrentLevel".ToString()));
	}
	public void LoadLevel(string LevelToLoad){
	
		Application.LoadLevel (LevelToLoad);
	}
	public void Exit(){
		
		Application.Quit();
	}
	public void EnglishLanguage(){
		
		LocalizationText.SetLanguage("EN");
		PlayerPrefs.SetString ("Language","EN");
		RefreshTexts ();
	}
	public void SpanishLanguage(){
		
		LocalizationText.SetLanguage("ES");
		PlayerPrefs.SetString ("Language","ES");
		RefreshTexts ();
	}
	public void RefreshTexts(){
		NewGameText.text=LocalizationText.GetText("NewGame");
		ContinueText.text=LocalizationText.GetText("Continue");
		LanguageText.text=LocalizationText.GetText("Language");
		ExitText.text=LocalizationText.GetText("Exit");
	}

	void OnGUI(){
		//if (GUI.Button (new Rect (10, 70, 50, 30), "Click")) {
		//	Debug.Log("Clicked the button with text");
		//}
			
	}// end of GUI
}
