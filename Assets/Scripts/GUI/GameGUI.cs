using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour {


	public Text InfoText;
	public Text GoldText;
	public Text CurrentWave;
	public GameObject CannonTurret;
	public GameObject MachineGunTurret;
	public GameObject AreaTurret;
    public GameObject FreezeTurret;
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

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			NormalSpeed ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			FastFoward ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			SuperFastFoward ();
		}
			

		GoldText.text = LocalizationText.GetText("Gold")+": "+ GlobalVariables.Money.ToString(); 
		CurrentWave.text = LocalizationText.GetText("Wave")+": "+ GlobalVariables.CurrentWave.ToString(); 

		if(GlobalVariables.LevelCleared==true){
			LevelClearedUI.SetActive(true);
		}
	} 
	void OnGUI()
	{

	

	}
	public void CreepSpawner(){
		GameObject myCreepSpawner = GameObject.FindGameObjectWithTag ("CreepSpawner");
		myCreepSpawner.SendMessage ("startCreeps");



	}
	public void InfiniteGold (){
		x++;
		Debug.Log (x);
		if(x==10){
			GlobalVariables.Money = 99999;
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
		

		#if UNITY_ANDROID
			Time.timeScale = 3.0F; 
		#else
			Time.timeScale = 3.0F;
		#endif

		#if UNITY_EDITOR 
		Time.timeScale = 15.0F;
		#endif

		 
		
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
		InfoText.text = LocalizationText.GetText("TurretCost")+": "+GlobalVariables.TurretCost.ToString ();

	}
	public void MachineGunTurretButton(){
		GlobalVariables.DestroyTurret=false;
		GlobalVariables.CurrentTurret = MachineGunTurret;
		SelectTurret cost = (SelectTurret) MachineGunTurret.GetComponent(typeof(SelectTurret));
		GlobalVariables.TurretCost = cost.getTurretCost();
		InfoText.text = LocalizationText.GetText("TurretCost")+": "+GlobalVariables.TurretCost.ToString ();

	}
	public void AreaTurretButton(){
		
		GlobalVariables.DestroyTurret=false;
		GlobalVariables.CurrentTurret = AreaTurret;
		SelectTurret cost = (SelectTurret) AreaTurret.GetComponent(typeof(SelectTurret));
		GlobalVariables.TurretCost = cost.getTurretCost();
		InfoText.text = LocalizationText.GetText("TurretCost")+": "+GlobalVariables.TurretCost.ToString ();
		
	}
    public void FreezeTurretButton(){
        GlobalVariables.DestroyTurret = false;
        GlobalVariables.CurrentTurret = FreezeTurret;
        SelectTurret cost = (SelectTurret)FreezeTurret.GetComponent(typeof(SelectTurret));
        GlobalVariables.TurretCost = cost.getTurretCost();
        InfoText.text = LocalizationText.GetText("TurretCost") + ": " + GlobalVariables.TurretCost.ToString();

    }
    public void DestroyTurretButton(){
		GlobalVariables.DestroyTurret=true; 
		InfoText.text = LocalizationText.GetText("DestroyTurret");
		GlobalVariables.CurrentTurret = null;
	}
	public void LoadLevel(string LevelToLoad){
		Application.LoadLevel (LevelToLoad); 
	}
	public void LoadNextLevel(){
		
		if (PlayerPrefs.GetInt ("NextLevel") == GlobalVariables.LastLevel+1) {
			Application.LoadLevel ("MainMenu"); 
		} else {
			Application.LoadLevel ("GeometricDefenseLvl"+PlayerPrefs.GetInt("NextLevel".ToString())); 
		}

	}
	public void getNextWave(string NextWaveCreepName ,Color CreepColor){ 
		
		if(NextWaveCreepName==" " || NextWaveCreepName==null){
			
			NextWaveType.GetComponent<Image>().color= Color.black; 
			NextWaveType.GetComponentInChildren<Text>().text=LocalizationText.GetText("LastWave"); 
		}else{
			NextWaveType.GetComponent<Image>().color= CreepColor; 
			Debug.Log (NextWaveCreepName);
			NextWaveType.GetComponentInChildren<Text>().text=LocalizationText.GetText("NextWave")+": "+ NextWaveCreepName; 
		}

	}
	public void CombinedWaves(){ 
		NextWaveType.GetComponent<Image>().color= Color.black; 
		NextWaveType.GetComponentInChildren<Text>().text=LocalizationText.GetText("CombinedWaves");  
	}
	public void UpgradeCurrentTurret(){ 
		if(CurrentTurret.myCurrentTurret != null){
			SelectTurret myTurret = CurrentTurret.myCurrentTurret.GetComponent<SelectTurret> ();
			myTurret.UpgradeTurret ();
			myTurret.UpdateMaterial (false);
			//CurrentTurret.myCurrentTurret.SendMessage ("UpgradeTurret");
//			Debug.Log("el nuevo nivel de la torre es!!!!! : "+CurrentTurret.myCurrentTurret.GetComponent<SelectTurret>().TurretLevel);
		}

	}
	public void DestroyElement(GameObject myObject){
		Destroy (myObject);
	}
	
}
