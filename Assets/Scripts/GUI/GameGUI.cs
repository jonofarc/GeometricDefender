using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class GameGUI : MonoBehaviour {


	public Text InfoText;
	public Text GoldText;
	public Text CurrentWave;
	public GameObject CannonTurret;
	public GameObject MachineGunTurret;
	public GameObject LevelClearedUI;
	public GameObject NextWaveType;

	private Lang LMan;
 	private int x=0;


	public void OnEnable()
	{	
		
		
		LMan = new Lang(Path.Combine(Application.dataPath+"/Resources/I18N", "MainGameTexts.xml"), PlayerPrefs.GetString("Language"), false);
		LMan.setLanguage(Path.Combine(Application.dataPath+"/Resources/I18N", "MainGameTexts.xml"), PlayerPrefs.GetString("Language"));
	}
	// Use this for initialization
	void Start () {

		GlobalVariables.LevelCleared = false;
		LevelClearedUI.SetActive (false);
		Time.timeScale = 1.0F;
	}
	
	// Update is called once per frame
	void Update () {


		GoldText.text = LMan.getString ("Gold")+": "+ GlobalVariables.Money.ToString(); 
		CurrentWave.text = LMan.getString ("Wave")+": "+ GlobalVariables.CurrentWave.ToString(); 
		if(GlobalVariables.LevelCleared==true){
			LevelClearedUI.SetActive(true);
		}
	}
	void OnGUI()
	{

	

	}
	public void InfiniteGold (){
		x++;
		Debug.Log (x);
		if(x==15){
			GlobalVariables.Money=99999;
			GlobalVariables.HP = 99999; 
		}

	}
	public void GameSpeed(){
		if(Time.timeScale == 1.0F){
			Time.timeScale = 2.0F;
		}else{

			Time.timeScale = 1.0F;
		}
	}
	public void Pause(){
		
		Time.timeScale = 0.0F;
		
	}
	public void FastFoward(){

			Time.timeScale = 2.0F;
		 
	}
	public void SuperFastFoward(){
		
		Time.timeScale = 10.0F;  
		
	}
	public void NormalSpeed(){
		
		Time.timeScale = 1.0F;
		
	}
	public void MainMenuButtton (){
		Application.LoadLevel("MainMenu");

	}
	public void CannonTurretButton(){

		GlobalVariables.DestroyTurret=false;
		GlobalVariables.CurrentTurret = CannonTurret;
		SelectTurret cost = (SelectTurret) CannonTurret.GetComponent(typeof(SelectTurret));
		GlobalVariables.TurretCost = cost.getTurretCost();
		InfoText.text = LMan.getString ("TurretCost")+": "+GlobalVariables.TurretCost.ToString ();

	}
	public void MachineGunTurretButton(){
		GlobalVariables.DestroyTurret=false;
		GlobalVariables.CurrentTurret = MachineGunTurret;
		SelectTurret cost = (SelectTurret) MachineGunTurret.GetComponent(typeof(SelectTurret));
		GlobalVariables.TurretCost = cost.getTurretCost();
		InfoText.text = LMan.getString ("TurretCost")+": "+GlobalVariables.TurretCost.ToString ();

	}
	public void DestroyTurretButton(){
		GlobalVariables.DestroyTurret=true; 
		InfoText.text = LMan.getString ("DestroyTurret");
		GlobalVariables.CurrentTurret = null;
	}
	public void LoadLevel(string LevelToLoad){
		Application.LoadLevel (LevelToLoad); 
	}
	public void LoadNextLevel(){
		if (PlayerPrefs.GetInt ("NextLevel") == 1) {
			Application.LoadLevel ("MainMenu"); 
		} else {
			Application.LoadLevel ("GeometricDefenseLvl"+PlayerPrefs.GetInt("NextLevel".ToString())); 
		}

	}
	public void getNextWave(string NextWaveCreepName ,Color CreepColor){
		NextWaveType.GetComponent<Image>().color= CreepColor; 
		NextWaveType.GetComponentInChildren<Text>().text=LMan.getString ("NextWave")+": \n"+ NextWaveCreepName; 
	}

}
