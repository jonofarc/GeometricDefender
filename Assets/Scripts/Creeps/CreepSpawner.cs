using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.IO;

public class CreepSpawner : MonoBehaviour {
	public bool CombinedWaves = false;
	public bool WaveLoop = false;

	[Header("All creep waves")]
	public GameObject[] BaseCreeps;
	public List<GameObject> CurrentCreeps;
	public GameObject[] BossCreeps;

	private GameObject CreepClone;
	//public float StartSpawnTime=3;
	public float spawnInterval=1;
	private float spawnIntervalOriginal=1;
	public bool SpawnActive=true; 
	public bool StartFirstWave=false; 
	public int CreepsSpawned=0;

	private int LevelClearedTimesCalled=0;
	public int WaveCreeps=10;
	public int CreepsSpawnedThisWave=0;
	public int CurrentWave=1;
	public float NextWaveTime=10;
	private float NextWaveTimeOriginal=10;
	public float waveHpPercentageIncrement=1f;
	[Header("growth curve values (higer values higher hp increases)")]
	public float HPIncrementcurbe=150f;
	public int waveCreepsIncrement=1;


	public TextAsset CreepColors;
	public TextAsset CreepSpawnList;

	private List<string> FileLines;
	private int CreepType = 0;
	private float elapsed = 0.0f; 
	private float elapsedNextWaveTime = 0.0f; 
	private int creepSeparator=0;
	private GameObject Canvas;
	private Color myCreepColor;

	// as of 3/18/2018 no better idea of how to yandle this comes to min
	[Header("All creep types")]
	public GameObject NomalCreep;
	public GameObject FastCreep;
	public GameObject StraightCreep;
	public GameObject ShieldCreep;
	public GameObject ForceFieldCreep;
	public GameObject MultiCreep;
	public GameObject ElementalAbsorbentCreep;
	public GameObject CreepBoss;


	// Use this for initialization
	void Start () {

		//Disable all Creeps
		GameObject[] Creeps = GameObject.FindGameObjectsWithTag("CreepG");
		foreach(GameObject creep in Creeps){
			creep.SetActive (false);
		}
		readCreepList ();
		spawnIntervalOriginal = spawnInterval;
		NextWaveTimeOriginal = NextWaveTime;

		GlobalVariables.GameStarted = false;
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
	void FixedUpdate () {

		elapsed += Time.deltaTime;
		if (elapsed > spawnInterval) {
			elapsed -= spawnInterval;
			if(SpawnActive && StartFirstWave){
				CreateCreep();
			}


		}

		//Check when it is time to spawn next wave

		if(CreepsSpawnedThisWave>=WaveCreeps && (CurrentWave<=BaseCreeps.Length || WaveLoop)){
			
			//Debug.Log ((elapsedNextWaveTime/GlobalVariables.GameSpeed)+"  :   "+NextWaveTime);
			elapsedNextWaveTime += Time.deltaTime;

			if((elapsedNextWaveTime)>NextWaveTime){
				elapsedNextWaveTime = 0;
				NextWave ();
			}
		} 

	}
	public void CreateCreep(){
		
		if (GlobalVariables.HP<=0){
			saveHighScores();
		}

		//we save BaseCreep number in case combinedwaves are on

		//we asign a random number to create a random creep
		int OriginalCreeptype=CreepType;
		if(BaseCreeps.Length>1 && CombinedWaves){

			CreepType=Random.Range(0, BaseCreeps.Length);
		}

		CreepClone = Instantiate(BaseCreeps[CreepType], BaseCreeps[CreepType].transform.position, BaseCreeps[CreepType].transform.rotation) as GameObject;
		//we separate spawn site to minimise physics calculation on runtime
		if(creepSeparator>16){
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
//			Debug.Log (BossCreeps.Length);
			for(int i=0; i<BossCreeps.Length; i++){
//				Debug.Log(BaseCreeps[CreepType].gameObject.name+"   :   "+BossCreeps[i].gameObject.name);
				if(BaseCreeps[CreepType].gameObject.name==BossCreeps[i].gameObject.name){
					//Debug.Log("this is a boss creep");
					CreepsSpawnedThisWave=WaveCreeps;
				}//end if 

			}//end for


		}// end if
		//we reasign CreepType to its originla value in case we have combinedwaves
		if(BaseCreeps.Length>1 && CombinedWaves){


			CreepType = OriginalCreeptype;
		}

		//Debug.Log("Time for next wave " + (CreepsSpawnedThisWave >= WaveCreeps && (CurrentWave <= BaseCreeps.Length || WaveLoop)));
		if (CreepsSpawnedThisWave>=WaveCreeps && (CurrentWave<=BaseCreeps.Length|| WaveLoop)){
			SpawnActive=false;

			Debug.Log("Time for next wave"+(spawnInterval*WaveCreeps));

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

		float increment = waveHpPercentageIncrement - Mathf.Sqrt(CurrentWave)/HPIncrementcurbe;
		//float increment = waveHpPercentageIncrement;
		Debug.Log (increment);
		for ( int i = 0; i < CurrentCreeps.Count; i++){
			CurrentCreeps[i].SetActive(true);
			CurrentCreeps[i].SendMessage("IncreaseLife",increment);
			CurrentCreeps[i].SetActive(false);
		
		}
		if (true) {
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
		GlobalVariables.GameStarted = true;
	}



	public void getNextWaveType(int CreepTypeColor){
		
		GameGUI GameGUIScript = (GameGUI) Canvas.GetComponent(typeof(GameGUI));


		if (CombinedWaves) {
			GameGUIScript.CombinedWaves();
		} else {
			myCreepColor =ReadFile (CreepTypeColor);


			
			GameGUIScript.getNextWave(BaseCreeps[CreepTypeColor].gameObject.name,myCreepColor); 
			//Debug.Log (BaseCreeps[CreepTypeColor].gameObject.name);
		}

	}


	
	public Color ReadFile(int CreepTypeColor)
	{
		Color myColor = new Color (0,0,0,0);
		FileLines = CreepColors.text.Split('\n',',').ToList();

		//if (fileLines.Contains(BaseCreeps[CreepType+1].name))
		//{
		//	myColor = new Color (1,1,1,1);
		//} 
		for(int i=0; i<FileLines.Count(); i++){

			if(FileLines[i].ToString()==BaseCreeps[CreepTypeColor].gameObject.name){

				myColor = new Color (float.Parse(FileLines[i+1]),float.Parse(FileLines[i+2]),float.Parse(FileLines[i+3]),float.Parse(FileLines[i+4])); 
			
			}
		}
		
		return myColor;

	}

	public void readCreepList(){
		FileLines = CreepSpawnList.text.Split('\n').ToList();
		//Check wich lines are valid
		List<GameObject> CreepWaveList = new List<GameObject>();
		for(int i=0; i<FileLines.Count(); i++){
			
			string Line = FileLines [i];

			if (!Line.Equals(" ") && !Line.StartsWith("#") && !Line.StartsWith(" ")) {
				
				//check wich creep is the line stating
				List<string> LineInformation = Line.Split(',').ToList();
				if(LineInformation.Count==2){
					//check wich type of creep 
					string CreepType = LineInformation [0];
					//check how many waves of that creep
					int CreepWaves = int.Parse(LineInformation [1]);
					// Add creep gameobject to list
					for(int j=0 ; j< CreepWaves; j++){
						switch(CreepType){
						case CreepsIndex.NomalCreep:
							CreepWaveList.Add (NomalCreep);
							break;
						case CreepsIndex.FastCreep:
							CreepWaveList.Add (FastCreep);
							break;
						case CreepsIndex.StraightCreep:
							CreepWaveList.Add (StraightCreep);
							break;
						case CreepsIndex.ShieldCreep:
							CreepWaveList.Add (ShieldCreep);
							break;
						case CreepsIndex.ForceFieldCreep:
							CreepWaveList.Add (ForceFieldCreep);
							break;
						case CreepsIndex.MultiCreep:
							CreepWaveList.Add (MultiCreep);
							break;
						case CreepsIndex.ElementalAbsorbentCreep:
							CreepWaveList.Add (ElementalAbsorbentCreep);
							break;
						case CreepsIndex.CreepBoss:
							CreepWaveList.Add (CreepBoss);
							break;
						}

					}
				}
			}
		}

		BaseCreeps = new GameObject[CreepWaveList.Count];
		for(int i=0; i<CreepWaveList.Count(); i++){
			BaseCreeps [i] = CreepWaveList [i];
		}

	}
	public void callLevelCleared(){
		Debug.Log ("Level Cleared");

		//ugly hack to avoid getting stuck after finishing a level becaue a creep is lost or stuck
		LevelClearedTimesCalled++;
		if(LevelClearedTimesCalled>20){
			
		}
		//check if final wave is destroyed
		SpawnActive=false; 
		GameObject[] AliveCreeps;
		AliveCreeps = GameObject.FindGameObjectsWithTag (GlobalVariables.CreepTag);

		Debug.Log (AliveCreeps.Length);
		if (AliveCreeps.Length<=0) {// +1 added to count the creepPathCheker



			GlobalVariables.LevelCleared = true;
			Debug.Log ("level is clear?  "+GlobalVariables.LevelCleared);
			saveHighScores();
		} else {
			// make sure creeps are not waiting without a direction
			foreach (GameObject creep in AliveCreeps) {
				Debug.Log ("REquesting patchcheck from Checkpath");
				creep.SendMessage ("PathCheck");
			}
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
