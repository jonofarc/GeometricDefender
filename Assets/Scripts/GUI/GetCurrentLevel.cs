﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEditor;


public class GetCurrentLevel : MonoBehaviour {
	public Text CurrentLevel;
	private Lang LMan;
	// Use this for initialization
	public void OnEnable()
	{	

		
		LMan = new Lang(Path.Combine(Application.dataPath+"/Resources/I18N", "MainGameTexts.xml"), PlayerPrefs.GetString("Language"), false);
		LMan.setLanguage(Path.Combine(Application.dataPath+"/Resources/I18N", "MainGameTexts.xml"), PlayerPrefs.GetString("Language"));
	}
	void Start () {
		Invoke ("DelayFunction",0.2f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void DelayFunction () {
		CurrentLevel.text = LMan.getString ("Level")+" : " + PlayerPrefs.GetInt ("CurrentLevel").ToString(); 
	}
}
