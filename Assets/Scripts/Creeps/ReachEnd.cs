﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class ReachEnd : MonoBehaviour {

	public int HP=20;
	public int DefaultDamage=1;
	public int StartMoney=20;
	private Text HPtext;
	public GameObject[] ReviveDependent;

	private AudioSource FinishTargetAudioSource;
	// Use this for initialization
	void Awake(){
		
	}
	void Start () {
		
		GlobalVariables.Revives = GlobalVariables.MaxRevives;
		HPtext= GameObject.FindGameObjectWithTag ("HP_Text").GetComponent<Text>();
		GlobalVariables.HP = HP;

		HPtext.text = LocalizationText.GetText("HP")+": " + HP.ToString ();
		GlobalVariables.Money = StartMoney;

		FinishTargetAudioSource = this.gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other) {
	
		if(other.gameObject.tag.Equals(GlobalVariables.CreepTag) || other.gameObject.tag.Equals("CreepF")){
			Destroy(other.gameObject);
			//	Debug.Log ("-1 hp");
			ReciveDamage(DefaultDamage);
			//ShowRewardedAd ();
		
		}

	}
	public void ReciveDamage(int Damage){

		//	Debug.Log ("-1 hp");
		GlobalVariables.HP=GlobalVariables.HP-Damage;
		HPtext.text = LocalizationText.GetText("HP")+": " + GlobalVariables.HP.ToString ();
		FinishTargetAudioSource.Play ();
		//	Debug.Log ("Remaining health "+HP);
		if(GlobalVariables.HP<=0){
			//Invoke("GameOver",1);
			GameOver();
		}
	}
	public void GameOver (){

		GlobalVariables.LevelFailed = true;
	
		Time.timeScale = 0.0F;

	

	


	}


	// this is the code used for adds
	public void ShowRewardedAd()
	{
		Debug.Log ("Showing add");
		//ApplyRewards ();
		//GlobalVariables.LevelFailed = false;
		if (Advertisement.IsReady("rewardedVideo"))
		{
			Debug.Log ("add ready");
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
			//Advertisement.Show();//add that can be skiped
		}else{
			Debug.Log ("add not ready");
		}


	}

	private void HandleShowResult(ShowResult result)
	{
		Debug.Log ("handling Showing add result");
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
			//
			// YOUR CODE TO REWARD THE GAMER
			// Give coins etc.
			ApplyRewards ();
			ReviveDependentReactivation ();
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			GlobalVariables.LevelFailed = true;
			break;
		}
	}
	public void ApplyRewards(){
		GlobalVariables.HP = GlobalVariables.HPReward;
		HPtext.text = LocalizationText.GetText("HP")+": " + GlobalVariables.HP.ToString ();

		//Activate Pause button here
		GameObject Canvas = GameObject.FindGameObjectWithTag ("Canvas");
		GameObject PauseButton = Canvas.transform.Find("SpeedButtons/Pause").gameObject;
		Toggle PauseButtonScript = PauseButton.GetComponent<Toggle> ();
		PauseButtonScript.isOn = true;

		GlobalVariables.Revives--;
		GlobalVariables.LevelFailed = false;

	}

	// not the best solution but being 7/20/2017 at 12:35 AM I cant think of other option is this or move the button so the add close button and this would not overlap
	public void ReviveDependentReactivation (){
		ReviveDependent = GameObject.FindGameObjectsWithTag ("ReviveDependent") ; 
		foreach (GameObject Dependent in ReviveDependent) {
			Dependent.GetComponent<Button> ().enabled = true;
		}

	}

}
