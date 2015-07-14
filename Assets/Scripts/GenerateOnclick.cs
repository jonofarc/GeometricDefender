using UnityEngine;
using System.Collections;

public class GenerateOnclick : MonoBehaviour {
	private GameObject PathCheker;
	private GameObject clone;

	public GameObject[] BuildBase;


	private int TurretToBuild=0;

	// Use this for initialization
	void Start () {

		BuildBase=GameObject.FindGameObjectsWithTag("Turret");
		PathCheker=GameObject.FindGameObjectWithTag("PathCheker");

	
	}
	
	// Update is called once per frame
	void Update () { 

	
	}
	void OnMouseDown(){
		BuildBase [TurretToBuild].SendMessage ("getCost");
		if (MoneyCount.Money >= MoneyCount.TurretCost) {

			MoneyCount.Money=MoneyCount.Money-MoneyCount.TurretCost;
			clone = Instantiate (BuildBase [TurretToBuild], BuildBase [TurretToBuild].transform.position, BuildBase [TurretToBuild].transform.rotation) as GameObject;
			clone.gameObject.SetActive (true);
			
			clone.transform.parent = this.transform;
			clone.transform.localScale = BuildBase [TurretToBuild].transform.localScale; 
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
	
		Destroy (clone);
	}
}
