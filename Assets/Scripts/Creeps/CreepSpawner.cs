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
		getNextWaveType ();


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



		if(BaseCreeps.Length>1 && CombinedWaves){

		//	CreepType=Random.Range(0, BaseCreeps.Length);
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
		if(CreepsSpawnedThisWave==WaveCreeps){
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
		if(CombinedWaves==false){
			CreepType++;

			// we set he name of the extwave creeps

			if(CreepType+1<BaseCreeps.Length){
				getNextWaveType();
			}
			else{

				GameGUI GameGUIScript = (GameGUI) Canvas.GetComponent(typeof(GameGUI));
				GameGUIScript.getNextWave(" " ,myCreepColor); 
			}

		
			if(CreepType==BaseCreeps.Length){
				if(WaveLoop){
					CreepType=0;
				}
				else{
					GlobalVariables.LevelCleared=true;
					SpawnActive=false;


				}

			}
		}
		if (GlobalVariables.LevelCleared == false) {
			//Invoke ("CreateCreep", spawnInterval);
			SpawnActive=true;
		} else {

		}


	}
	public void startCreeps(){
		StartFirstWave = true;
	}
	public void getNextWaveType(){
	
		myCreepColor =ReadFile ();
		//GlobalVariables.NextWaveCreepName=BaseCreeps[CreepType+1].gameObject.name;
		//SelectTurret cost = (SelectTurret) CannonTurret.GetComponent(typeof(SelectTurret));
		GameGUI GameGUIScript = (GameGUI) Canvas.GetComponent(typeof(GameGUI));

		GameGUIScript.getNextWave(BaseCreeps[CreepType+1].gameObject.name,myCreepColor); 
	}


	
	public Color ReadFile()
	{
		Color myColor = new Color (0,0,0,0);
		fileLines = CreepColors.text.Split('\n',',').ToList();

		//if (fileLines.Contains(BaseCreeps[CreepType+1].name))
		//{
		//	myColor = new Color (1,1,1,1);
		//} 
		for(int i=0; i<fileLines.Count(); i++){

			if(fileLines[i].ToString()==BaseCreeps[CreepType+1].gameObject.name){

				myColor = new Color (float.Parse(fileLines[i+1]),float.Parse(fileLines[i+2]),float.Parse(fileLines[i+3]),float.Parse(fileLines[i+4])); 
			
			}
		}
		
		return myColor;

	}
	
	
	
	
}
