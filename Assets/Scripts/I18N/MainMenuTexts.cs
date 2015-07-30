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
	private string currentLang = "Spanish";
	
	// Use this for initialization
	
	
	
	public void OnEnable()
	{
		LMan = new Lang(Path.Combine(Application.dataPath+"/Resources/I18N", "MainGameTexts.xml"), currentLang, false);
		LMan.setLanguage(Path.Combine(Application.dataPath+"/Resources/I18N", "MainGameTexts.xml"), currentLang);
	}
	void Start () {
		refreshTexts ();
		
	}
	public void EnglishLanguage(){
		currentLang = "English";
		LMan.setLanguage(Path.Combine(Application.dataPath+"/Resources/I18N", "MainGameTexts.xml"), currentLang);
		refreshTexts ();
	}
	public void SpanishLanguage(){
		currentLang = "Spanish";
		LMan.setLanguage(Path.Combine(Application.dataPath+"/Resources/I18N", "MainGameTexts.xml"), currentLang);
		refreshTexts ();
	}
	public void refreshTexts(){
		//myText.text = LMan.getString ("NewGame");
		NewGameText.text= LMan.getString ("NewGame");
		ContinueText.text= LMan.getString ("Continue");
		LanguageText.text= LMan.getString ("Language");
		ExitText.text= LMan.getString ("Exit");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
