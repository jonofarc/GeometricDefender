using UnityEngine;
using System.Collections;

public class MultiCreep : MonoBehaviour {

	public GameObject miniCreeps;
	int currentMiniCreep=0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void CreepEfect(){

		if(miniCreeps!=null){

			Debug.Log(miniCreeps.transform.childCount);

			currentMiniCreep=miniCreeps.transform.childCount-1;
			for(int i=miniCreeps.transform.childCount-1; i>=0; i--){
				//miniCreeps.transform.GetChild(i).transform.parent=this.transform.parent;
				Invoke("moveMiniCreeps",(i*0.1f)); 


			}
			//Destroy(this.gameObject);
		}

	}
	void moveMiniCreeps () {
		Debug.Log (currentMiniCreep);
		if(currentMiniCreep>=0){
			miniCreeps.transform.GetChild (currentMiniCreep).gameObject.SetActive (true);
			miniCreeps.transform.GetChild(currentMiniCreep).transform.parent=this.transform.parent;
			currentMiniCreep--;
			if(currentMiniCreep<=0){
				Destroy(this.gameObject);
			}
		}

	}

}
