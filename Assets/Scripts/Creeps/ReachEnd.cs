using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReachEnd : MonoBehaviour {

	public int HP=20;
	public int DefaultDamage=1;
	public int StartMoney=20;
	private Text HPtext;
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
			Invoke("GameOver",1);
		}
	}
	public void GameOver (){
		GlobalVariables.LevelFailed = true;
		Time.timeScale = 0.0F;
	}
}
