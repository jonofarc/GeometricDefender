using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {

	public GameObject ContinueButton;

	public Text NewGameText;
	public Text ContinueText;
	public Text InfiniteText;
	public Text LanguageText;
	public Text ExitText;
    public Text LoadingText;
    public Text OptionsText;
    public GameObject FPS30;
    public GameObject FPS60;

    // Use this for initialization
    void Start () {
		//Debug.Log (_language); 
		//language section //we make sure that the language is a valid one and that its on the latest language used
	

		// show continue button only if current level is more than the second one
		if (PlayerPrefs.GetInt ("CurrentLevel") > 0) {
			ContinueButton.SetActive (true);
		} else {
			ContinueButton.SetActive (false);
		}

        //check if preferences for fps exist
        Debug.Log(PlayerPrefs.GetInt("TargetFPS"));
        if (PlayerPrefs.GetInt("TargetFPS") != 30 && PlayerPrefs.GetInt("TargetFPS") != 60)
        {
            Debug.Log("no specified fps, setting default fps 30");
            PlayerPrefs.SetInt("TargetFPS", 30);

        } else if (PlayerPrefs.GetInt("TargetFPS") == 30) {

            FPS30.GetComponent<Toggle>().isOn = true;
            FPS60.GetComponent<Toggle>().isOn = false;

        }
        else if (PlayerPrefs.GetInt("TargetFPS") == 60)
        {

            FPS60.GetComponent<Toggle>().isOn = true;
            FPS30.GetComponent<Toggle>().isOn = false;

        }
        // setting default fps
        GlobalVariables.TargetFPS = PlayerPrefs.GetInt("TargetFPS");
        Application.targetFrameRate = GlobalVariables.TargetFPS;

        

        //change text of objects to current language

        RefreshTexts ();

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Continue(){
		
		Application.LoadLevelAsync("GeometricDefenseLvl"+PlayerPrefs.GetInt("CurrentLevel".ToString()));
	}
	public void LoadLevel(string LevelToLoad){
	
		Application.LoadLevelAsync (LevelToLoad);
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
		InfiniteText.text=LocalizationText.GetText("Infinite");
		
		ExitText.text=LocalizationText.GetText("Exit");
        LoadingText.text = LocalizationText.GetText("Loading"); 
        OptionsText.text = LocalizationText.GetText("Options");
        LanguageText.text = LocalizationText.GetText("Language");
    }

	void OnGUI(){
		//if (GUI.Button (new Rect (10, 70, 50, 30), "Click")) {
		//	Debug.Log("Clicked the button with text");
		//}
			
	}// end of GUI

    public void setFPS(int TargetFPS) {
        PlayerPrefs.SetInt("TargetFPS", TargetFPS);
        GlobalVariables.TargetFPS = PlayerPrefs.GetInt("TargetFPS");
        Debug.Log("Current FPS set to: "+GlobalVariables.TargetFPS);
        Application.targetFrameRate = GlobalVariables.TargetFPS;
    }
}
