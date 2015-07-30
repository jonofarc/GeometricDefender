using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class TranslateThis : MonoBehaviour {
	private Lang LMan;

	public string Text;//this variable is the name of the string to get in XML language file
	public void OnEnable()
	{	
		LMan = new Lang(Path.Combine(Application.dataPath+"/Resources/I18N", "MainGameTexts.xml"), PlayerPrefs.GetString("Language"), false);
		LMan.setLanguage(Path.Combine(Application.dataPath+"/Resources/I18N", "MainGameTexts.xml"), PlayerPrefs.GetString("Language"));
	}
	// Use this for initialization
	void Start () {
		Text thisText = gameObject.GetComponent<Text> ();
		thisText.text= LMan.getString (Text);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
