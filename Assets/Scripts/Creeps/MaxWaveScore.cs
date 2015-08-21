using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MaxWaveScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("setText",0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void setText(){
		gameObject.GetComponent<Text>().text=LocalizationText.GetText("MaximiunWave")+": "+PlayerPrefs.GetInt(Application.loadedLevelName+"Wave").ToString();

	}
}
