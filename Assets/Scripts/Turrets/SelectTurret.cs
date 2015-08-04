using UnityEngine;
using System.Collections;

public class SelectTurret : MonoBehaviour {
	public int TurretCost=10;
	public GameObject RangeAura;
	public float SelectedTime=5f;
	public GameObject Base;
	private GameObject[] Auras;
	public Material SelectMaterial;
	private Material OriginalMaterial;
	// Use this for initialization
	void Start () {
		SelectMaterial=(Material)Resources.Load("Materials/SelectedTurret");
		OriginalMaterial = Base.GetComponent<Renderer> ().material;
	} 
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown(){
		if(GlobalVariables.DestroyTurret==false){
			CurrentTurret.myCurrentTurret = this.gameObject;
			DisableAura();
			Base.GetComponent<Renderer> ().material = SelectMaterial;
			RangeAura.GetComponent<Renderer>().enabled=true;
			Invoke ("DisableAura",SelectedTime);
		}else{
			DestroyTurret();
		}

	
		
	}
	void resetMaterial(){
		Base.GetComponent<Renderer> ().material = OriginalMaterial;
	}
	void getCost(){
		GlobalVariables.TurretCost = TurretCost;
	}
	void DestroyTurret(){
		GlobalVariables.Money = GlobalVariables.Money + ((int)TurretCost / 2);
		Destroy (this.gameObject);
	}
	public int getTurretCost(){
		Debug.Log (this.gameObject.name);
		return TurretCost;
	}
	public void DisableAura(){
		Auras= GameObject.FindGameObjectsWithTag ("RangeAura");
		foreach (GameObject Aura in Auras) {
			Aura.GetComponent<Renderer>().enabled=false;
			Aura.SendMessageUpwards("resetMaterial");
		}
	}
}
 