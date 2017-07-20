using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHandler : MonoBehaviour {
	private GameObject Finish; 
	GameObject[] ReviveDependent;
	// Use this for initialization
	void Start () {
		ReviveDependent = GameObject.FindGameObjectsWithTag ("ReviveDependent") ; 
		Finish = GameObject.FindGameObjectWithTag ("Finish"); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void RetryLevel (){
		
	}
	public void WatchAdd (){
		Debug.Log ("The amount of revives is "+GlobalVariables.Revives);
		Finish.SendMessage ("ShowRewardedAd");
		if(GlobalVariables.Revives<=0){
			
			foreach (GameObject Dependent in ReviveDependent) {
				Dependent.SetActive (false);
			}
		}
	}
}
