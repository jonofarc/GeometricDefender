using UnityEngine;
using System.Collections;

public class GenerateOnclick : MonoBehaviour {
	private GameObject PathCheker;
	private GameObject clone;






	// Use this for initialization
	void Start () {


		PathCheker=GameObject.FindGameObjectWithTag("PathCheker");

	
	}
	
	// Update is called once per frame
	void Update () { 

	
	}
	void OnMouseDown(){

		if (GlobalVariables.Money >= GlobalVariables.TurretCost) {

			GlobalVariables.Money=GlobalVariables.Money-GlobalVariables.TurretCost;
			clone = Instantiate (GlobalVariables.CurrentTurret, GlobalVariables.CurrentTurret.transform.position, GlobalVariables.CurrentTurret.transform.rotation) as GameObject;
			clone.gameObject.SetActive (true);
			 
			clone.transform.parent = this.transform;
			clone.transform.localScale = GlobalVariables.CurrentTurret.transform.localScale; 
			clone.transform.localPosition = new Vector3 (0, -0.49f, 0);
			clone.name = clone.name + (CurrentTurret.TurretNumber.ToString ());
			CurrentTurret.TurretNumber++;
			
			PathCheker.SendMessage ("SetLatestTurret", this.gameObject);
		} else {
			Debug.Log("no price set for turret or selected turret not valid");
		}

	
	


	}
	void ReverseTurret(){
		Debug.Log ("Destruyendo");
		if(clone != null){
			Destroy (clone);
		}

	}
}
