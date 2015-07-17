﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReachEnd : MonoBehaviour {

	public int HP=20;
	public int StartMoney=20;
	public Text HPtext;
	// Use this for initialization
	void Start () {
		GlobalVariables.HP = HP;
		HPtext.text = "Vidas: " + HP.ToString ();
		GlobalVariables.Money = StartMoney;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other) {
	
		if(other.gameObject.tag=="CreepG" || other.gameObject.tag=="CreepF"){
			Destroy(other.gameObject);
			//	Debug.Log ("-1 hp");
			GlobalVariables.HP=GlobalVariables.HP-1;
			HPtext.text = "Vidas: " + GlobalVariables.HP.ToString ();
			//	Debug.Log ("Remaining health "+HP);
			if(GlobalVariables.HP<=0){
				Application.LoadLevel("GameOver");
			}
		}

	}
}
