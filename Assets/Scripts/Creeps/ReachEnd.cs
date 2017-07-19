using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class ReachEnd : MonoBehaviour {

	public int HP=20;
	public int DefaultDamage=1;
	public int StartMoney=20;
	private Text HPtext;
	public bool ReviveUsed = false;
	// Use this for initialization
	void Start () {
		HPtext= GameObject.FindGameObjectWithTag ("HP_Text").GetComponent<Text>();
		GlobalVariables.HP = HP;

		HPtext.text = LocalizationText.GetText("HP")+": " + HP.ToString ();
		GlobalVariables.Money = StartMoney;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other) {
	
		if(other.gameObject.tag=="CreepG" || other.gameObject.tag=="CreepF"){
			Destroy(other.gameObject);
			//	Debug.Log ("-1 hp");
			ReciveDamage(DefaultDamage);
		
		}

	}
	public void ReciveDamage(int Damage){

		//	Debug.Log ("-1 hp");
		GlobalVariables.HP=GlobalVariables.HP-Damage;
		HPtext.text = LocalizationText.GetText("HP")+": " + GlobalVariables.HP.ToString ();
		//	Debug.Log ("Remaining health "+HP);
		if(GlobalVariables.HP<=0){
			//Invoke("GameOver",1);
			GameOver();
		}
	}
	public void GameOver (){
		if (ReviveUsed) {
			GlobalVariables.LevelFailed = true;
			Time.timeScale = 0.0F;
		} else {
			Time.timeScale = 0.0F;
			ShowRewardedAd ();
		}


	}


	// this is the code used for adds
	public void ShowRewardedAd()
	{
		if (Advertisement.IsReady("rewardedVideo"))
		{
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
			//
			// YOUR CODE TO REWARD THE GAMER
			// Give coins etc.
			GlobalVariables.HP = 5;
			HPtext.text = LocalizationText.GetText("HP")+": " + GlobalVariables.HP.ToString ();
			ReviveUsed = true;
			Time.timeScale = 1.0f;
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}

}
