using UnityEngine;
using System.Collections;

public class LanguageSet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetString ("Language")!="ES"&&PlayerPrefs.GetString ("Language")!="EN"){
			Debug.Log("no tiene ninguno de los idiomas predefinidos");
			PlayerPrefs.SetString("Language","EN");
		}
		LocalizationText.SetLanguage(PlayerPrefs.GetString ("Language"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
