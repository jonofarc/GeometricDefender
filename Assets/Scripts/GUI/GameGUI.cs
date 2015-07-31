using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour {


	public Text InfoText;
	public Text GoldText;
	public Text CurrentWave;
	public GameObject CannonTurret;
	public GameObject MachineGunTurret;
	public GameObject LevelClearedUI;
	public GameObject NextWaveType;
 

	private int x=0;


	// Use this for initialization
	void Start () {

		GlobalVariables.LevelCleared = false;
		LevelClearedUI.SetActive (false);
		Time.timeScale = 1.0F;
	}
	
	// Update is called once per frame
	void Update () {


		GoldText.text = "Oro: "+ GlobalVariables.Money.ToString(); 
		CurrentWave.text = "Oleada: "+ GlobalVariables.CurrentWave.ToString(); 
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
		if(x==10){
			GlobalVariables.Money=99999;
		}
		GlobalVariables.HP = 9999; 
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
		InfoText.text = "Costo Torre: "+GlobalVariables.TurretCost.ToString ();

	}
	public void MachineGunTurretButton(){
		GlobalVariables.DestroyTurret=false;
		GlobalVariables.CurrentTurret = MachineGunTurret;
		SelectTurret cost = (SelectTurret) MachineGunTurret.GetComponent(typeof(SelectTurret));
		GlobalVariables.TurretCost = cost.getTurretCost();
		InfoText.text = "Costo Torre: "+GlobalVariables.TurretCost.ToString ();

	}
	public void DestroyTurretButton(){
		GlobalVariables.DestroyTurret=true; 
		InfoText.text = "Selecciona torre a destruir (se regresara la mitad del costo de la torre)";
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
		NextWaveType.GetComponentInChildren<Text>().text="Siguiente Oleada: \n"+ NextWaveCreepName; 
	}

}
