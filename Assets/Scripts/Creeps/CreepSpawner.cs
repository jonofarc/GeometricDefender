using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreepSpawner : MonoBehaviour {
	public bool CombinedWaves = false;
	public GameObject[] BaseCreeps;
	public List<GameObject> CurrentCreeps;
	private GameObject CreepClone;
	//public float StartSpawnTime=3;
	public float spawnInterval=1;
	public bool SpawnActive=true; 
	public int CreepsSpawned=0;

	public int WaveCreeps=10;
	public int CreepsSpawnedThisWave=0;
	public int CurrentWave=1;
	public int NextWaveTime=10;
	public float waveHpPercentageIncrement=1f;
	public int waveCreepsIncrement=1;

	private int CreepType = 0;
	private float elapsed = 0.0f; 

	// Use this for initialization
	void Start () {
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
			if(SpawnActive){
				CreateCreep();
			}

			
		}

	}
	public void CreateCreep(){

		Debug.Log (CreepType);

		if(BaseCreeps.Length>1 && CombinedWaves){
			CreepType=Random.Range(0, BaseCreeps.Length);
		}

		CreepClone = Instantiate(BaseCreeps[CreepType], BaseCreeps[CreepType].transform.position, BaseCreeps[CreepType].transform.rotation) as GameObject;
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
		SpawnActive=true;
		CreepsSpawnedThisWave = 0;
	
		if(spawnInterval<0.1f){
			spawnInterval=0.1f;
		}
		WaveCreeps= WaveCreeps+waveCreepsIncrement;
		Invoke ("CreateCreep",spawnInterval);


		//we increase creep hp once per type
		
		for ( int i = 0; i < CurrentCreeps.Count; i++){
			CurrentCreeps[i].SetActive(true);
			CurrentCreeps[i].SendMessage("IncreaseLife",waveHpPercentageIncrement);
			CurrentCreeps[i].SetActive(false);
		
		}
		if(CombinedWaves==false){
			CreepType++;
			if(CreepType==BaseCreeps.Length){
				CreepType=0;
			}
		}

	}
	public void startCreeps(){
		SpawnActive = true;
	}
}
