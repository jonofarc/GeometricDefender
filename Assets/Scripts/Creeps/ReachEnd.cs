using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;


public class ReachEnd : MonoBehaviour {

	public int HP=20;
	public int DefaultDamage=1;
	public int StartMoney=20;
	public Text HPtext;
	 
	private Lang LMan;



	public void OnEnable()
	{	
		
		
		LMan = new Lang(Path.Combine(Application.dataPath+"/Resources/I18N", "MainGameTexts.xml"), PlayerPrefs.GetString("Language"), false);
		LMan.setLanguage(Path.Combine(Application.dataPath+"/Resources/I18N", "MainGameTexts.xml"), PlayerPrefs.GetString("Language"));
	}
	// Use this for initialization
	void Start () {
		GlobalVariables.HP = HP;
		HPtext.text = LMan.getString ("HP")+": " + HP.ToString ();
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
		HPtext.text = LMan.getString ("HP")+": "+ GlobalVariables.HP.ToString ();
		//	Debug.Log ("Remaining health "+HP);
		if(GlobalVariables.HP<=0){
			Application.LoadLevel("GameOver");
		}
	}
}
