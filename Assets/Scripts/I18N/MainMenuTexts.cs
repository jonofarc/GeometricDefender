using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine.UI;

public class MainMenuTexts : MonoBehaviour {

	
	public Text NewGameText;
	public Text ContinueText;
	public Text LanguageText;
	public Text ExitText;

	private Lang LMan;

	
	// Use this for initialization
	
	
	
	public void OnEnable()
	{	
	

		LMan = new Lang(Path.Combine(Application.dataPath+"/Resources/I18N", "MainGameTexts.xml"), PlayerPrefs.GetString("Language"), false);
		LMan.setLanguage(Path.Combine(Application.dataPath+"/Resources/I18N", "MainGameTexts.xml"), PlayerPrefs.GetString("Language"));
	}
	void Start () {
		if (PlayerPrefs.GetString ("Language") == null) {
			
			PlayerPrefs.SetString ("Language", "Spanish");
			Debug.Log(PlayerPrefs.GetString("Language"));
		}
		refreshTexts ();
		
	}
	public void EnglishLanguage(){ 
		PlayerPrefs.SetString("Language","English");
		LMan.setLanguage(Path.Combine(Application.dataPath+"/Resources/I18N", "MainGameTexts.xml"), PlayerPrefs.GetString("Language"));
		refreshTexts ();
	}
	public void SpanishLanguage(){
		PlayerPrefs.SetString("Language","Spanish");
		LMan.setLanguage(Path.Combine(Application.dataPath+"/Resources/I18N", "MainGameTexts.xml"), PlayerPrefs.GetString("Language"));
		refreshTexts ();
	}
	public void refreshTexts(){
	
		NewGameText.text= LMan.getString ("NewGame");
		ContinueText.text= LMan.getString ("Continue");
		LanguageText.text= LMan.getString ("Language");
		ExitText.text= LMan.getString ("Exit");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
