using UnityEngine;
using System.Collections;

public class CreepSpawner : MonoBehaviour {
	public GameObject[] BaseCreeps;
	private GameObject CreepClone;
	public float StartSpawnTime=3;
	public float spawnInterval=1;
	public bool SpawnActive=true; 
	public int CreepsSpawned=0;

	public int WaveCreeps=10;
	public int CurrentWave=0;
	public int NextWaveTime=10;
	public float waveHpIncrement=1f;


	private float elapsed = 0.0f; 

	// Use this for initialization
	void Start () {

		Invoke ("CreateCreep",StartSpawnTime);
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
	void CreateCreep(){


		int CreepType = 0;
		if(BaseCreeps.Length>1){
			CreepType=Random.Range(0, BaseCreeps.Length);
		}

		CreepClone = Instantiate(BaseCreeps[CreepType], BaseCreeps[CreepType].transform.position, BaseCreeps[CreepType].transform.rotation) as GameObject;
		CreepClone.gameObject.SetActive(true);
		
		CreepClone.transform.parent=this.transform;
		CreepClone.transform.localScale=BaseCreeps[CreepType].transform.localScale; 

		CreepsSpawned++;
		CreepClone.name = BaseCreeps [CreepType] + CreepsSpawned.ToString ();

		CurrentWave++;
		if(CurrentWave==WaveCreeps){
			SpawnActive=false;
			Invoke("NextWave",NextWaveTime);
		//	Debug.Log("Time for next wave"+(spawnInterval*WaveCreeps));

		}




	}
	void NextWave(){

		SpawnActive=true;
		CurrentWave = 0;
	
		if(spawnInterval<0.1f){
			spawnInterval=0.1f;
		}
		WaveCreeps++;
		Invoke ("CreateCreep",spawnInterval);

		for(int i=0; i<BaseCreeps.Length; i++){
			BaseCreeps[i].SetActive(true);
			BaseCreeps[i].SendMessage("IncreaseLife",waveHpIncrement);
			BaseCreeps[i].SetActive(false);
		}

	}
}
