using UnityEngine;
using System.Collections;

using System.IO;
using UnityEditor;
using UnityEngine.UI;

public class ChangeTexts : MonoBehaviour {

	public Text myText;
	private Lang LMan;
	private string currentLang = "English";

	// Use this for initialization



	public void OnEnable()
	{
		/*
    Initialize the Lang class by providing a path to the desired language XML file, a default language
    and a boolean to indicate if we are operating on an XML file located from a downloaded resource or local.
    True if XML resource is on the web, false if local
 
    If initializing from a web based XML resource you'll need to supply the text of the downloaded resource in placed
    of the path.
 
    web example:
    var wwwXML : WWW = new WWW("http://www.exampleURL.com/lang.xml");
    yield wwwXML;
     
    LMan = new Lang(wwwXML.text, currentLang, true);
    */
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

		myText.text = LMan.getString ("NewGame");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
