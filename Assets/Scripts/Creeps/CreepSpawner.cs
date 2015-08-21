using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.IO;

public class CreepSpawner : MonoBehaviour {
	public bool CombinedWaves = false;
	public bool WaveLoop = false;
	public GameObject[] BaseCreeps;
	public List<GameObject> CurrentCreeps;
	public GameObject[] BossCreeps;

	private GameObject CreepClone;
	//public float StartSpawnTime=3;
	public float spawnInterval=1;
	public bool SpawnActive=true; 
	public bool StartFirstWave=false; 
	public int CreepsSpawned=0;

	public int WaveCreeps=10;
	public int CreepsSpawnedThisWave=0;
	public int CurrentWave=1;
	public int NextWaveTime=10;
	public float waveHpPercentageIncrement=1f;
	public int waveCreepsIncrement=1;


	public TextAsset CreepColors;

	private List<string> fileLines;
	private int CreepType = 0;
	private float elapsed = 0.0f; 
	private int creepSeparator=0;
	private GameObject Canvas;
	private Color myCreepColor;


	// Use this for initialization
	void Start () {
		//we set nextWaveType

		Canvas = GameObject.FindGameObjectWithTag ("Canvas"); 
		getNextWaveType (CreepType + 1);
		



		GlobalVariables.CurrentWave = CurrentWave;
		// we create a list of the unique creep types in this level
		for(int i=0; i<BaseCreeps.Length; i++){
			bool AlreadyIn=false;
			if (CurrentCreeps.Contains(BaseCreeps[i]))
			{
				AlreadyIn=true;
			}

			if(AlreadyIn==false){
				CurrentCreeps.Add(BaseCreeps[i]);
			}

		}
		
	
		//	Invoke ("startCreeps",StartSpawnTime);
	}
	
	// Update is called once per frame
	void Update () {

		elapsed += Time.deltaTime;
		if (elapsed > spawnInterval) {
			elapsed -= spawnInterval;
			if(SpawnActive && StartFirstWave){
				CreateCreep();
			}

			
		}

	}
	public void CreateCreep(){

		if(GlobalVariables.HP<=0){
			saveHighScores();
		}


		if(BaseCreeps.Length>1 && CombinedWaves){

			CreepType=Random.Range(0, BaseCreeps.Length);
		}

		CreepClone = Instantiate(BaseCreeps[CreepType], BaseCreeps[CreepType].transform.position, BaseCreeps[CreepType].transform.rotation) as GameObject;
		//we separate spawn site to minimise physics calculation on runtime
		if(creepSeparator>20){
			creepSeparator=0;
		}
		CreepClone.transform.position = new Vector3 (CreepClone.transform.position.x+creepSeparator,CreepClone.transform.position.y,CreepClone.transform.position.z);
		creepSeparator=creepSeparator+2;

		CreepClone.gameObject.SetActive(true);
		
		CreepClone.transform.parent=this.transform;
		CreepClone.transform.localScale=BaseCreeps[CreepType].transform.localScale; 

		CreepsSpawned++;
		CreepClone.name = BaseCreeps [CreepType] + CreepsSpawned.ToString ();

		CreepsSpawnedThisWave++;

		// we check to see if this wave is a boss wave

	

		if(BossCreeps.Length>=0){
			Debug.Log (BossCreeps.Length);
			for(int i=0; i<BossCreeps.Length; i++){
				Debug.Log(BaseCreeps[CreepType].gameObject.name+"   :   "+BossCreeps[i].gameObject.name);
				if(BaseCreeps[CreepType].gameObject.name==BossCreeps[i].gameObject.name){
					Debug.Log("this is a boss creep");
					CreepsSpawnedThisWave=WaveCreeps;
				}//end if 

			}//end for


		}// end if
		//if(BaseCreeps[CreepType]==){

		//}

		if(CreepsSpawnedThisWave==WaveCreeps && (CurrentWave<=BaseCreeps.Length|| WaveLoop)){
			SpawnActive=false;
			Invoke("NextWave",NextWaveTime);

		//	Debug.Log("Time for next wave"+(spawnInterval*WaveCreeps));

		} 




	}
	public void NextWave(){


		CurrentWave++;
		GlobalVariables.CurrentWave = CurrentWave;

		CreepsSpawnedThisWave = 0;
	
		if(spawnInterval<0.1f){
			spawnInterval=0.1f;
		}
		WaveCreeps= WaveCreeps+waveCreepsIncrement;



		//we increase creep hp once per type
		
		for ( int i = 0; i < CurrentCreeps.Count; i++){
			CurrentCreeps[i].SetActive(true);
			CurrentCreeps[i].SendMessage("IncreaseLife",waveHpPercentageIncrement);
			CurrentCreeps[i].SetActive(false);
		
		}
		if (CombinedWaves == false) {
			CreepType++;

			// we set he name of the extwave creeps



		
			if (CreepType == BaseCreeps.Length) { 
				if (WaveLoop) {
					Debug.Log ("reseteando creep type");

					CreepType = 0;
				} else {
					 
					Invoke ("callLevelCleared", 2);

				}

			}

			if (CreepType + 1 < BaseCreeps.Length) {
				getNextWaveType (CreepType + 1);
			}// end CreepType+1<BaseCreeps.Length
			else {  
				if (WaveLoop) {  
					getNextWaveType (0);//since we are in a loop when we reach the last element we call the information of the first
				} else {
					GameGUI GameGUIScript = (GameGUI)Canvas.GetComponent (typeof(GameGUI));
					GameGUIScript.getNextWave (" ", myCreepColor); 
				}
				
				
			}//end else CreepType+1<BaseCreeps.Length 
		
		}//end CombinedWaves == false 
		else {
			getNextWaveType (0);
		}// end else CombinedWaves == false
 
		if (GlobalVariables.LevelCleared == false &&CreepType!=BaseCreeps.Length) {

			SpawnActive=true;
		} else { 

		}
		 

	}
	public void startCreeps(){
		StartFirstWave = true;
	}
	public void getNextWaveType(int CreepTypeColor){ 
		GameGUI GameGUIScript = (GameGUI) Canvas.GetComponent(typeof(GameGUI));
		if (CombinedWaves) {
			GameGUIScript.CombinedWaves();
		} else {
			myCreepColor =ReadFile (CreepTypeColor);


			
			GameGUIScript.getNextWave(BaseCreeps[CreepTypeColor].gameObject.name,myCreepColor); 
		}

	}


	
	public Color ReadFile(int CreepTypeColor)
	{
		Color myColor = new Color (0,0,0,0);
		fileLines = CreepColors.text.Split('\n',',').ToList();

		//if (fileLines.Contains(BaseCreeps[CreepType+1].name))
		//{
		//	myColor = new Color (1,1,1,1);
		//} 
		for(int i=0; i<fileLines.Count(); i++){

			if(fileLines[i].ToString()==BaseCreeps[CreepTypeColor].gameObject.name){

				myColor = new Color (float.Parse(fileLines[i+1]),float.Parse(fileLines[i+2]),float.Parse(fileLines[i+3]),float.Parse(fileLines[i+4])); 
			
			}
		}
		
		return myColor;

	}
	public void callLevelCleared(){
		//check if final wave is destroyed
		SpawnActive=false;
	
		if (this.transform.childCount <= (CurrentCreeps.Count())+1) {// +1 added to count the creepPathCheker



			GlobalVariables.LevelCleared = true;
			saveHighScores();
		} else {
			Invoke("callLevelCleared",2);
		}


	}

	public void saveHighScores(){


		Debug.Log ("gravando highscores");
		if(PlayerPrefs.GetInt(Application.loadedLevelName+"Wave") != null){
			if(PlayerPrefs.GetInt(Application.loadedLevelName+"Wave")<=(CurrentWave-1)){
				PlayerPrefs.SetInt(Application.loadedLevelName+"Wave",(CurrentWave-1));
				Debug.Log("new wave score:"+PlayerPrefs.GetInt(Application.loadedLevelName+"Wave"));
				if(PlayerPrefs.GetInt(Application.loadedLevelName+"HP")<=GlobalVariables.HP){
					PlayerPrefs.SetInt(Application.loadedLevelName+"HP",GlobalVariables.HP);

					Debug.Log("new hp score:"+PlayerPrefs.GetInt(Application.loadedLevelName+"HP"));
				}
				
				
			} 
			
		}
		else{
			PlayerPrefs.SetInt(Application.loadedLevelName+"Wave",0);
			PlayerPrefs.SetInt(Application.loadedLevelName+"HP",0);
		}




	}
	
	
	
	
}
