﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetCurrentLevel : MonoBehaviour {
	public Text CurrentLevel;
	// Use this for initialization
	void Start () {
		Invoke ("DelayFunction",0.2f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void DelayFunction () {
		CurrentLevel.text = "Nivel: " + PlayerPrefs.GetInt ("CurrentLevel").ToString(); 
	}
}