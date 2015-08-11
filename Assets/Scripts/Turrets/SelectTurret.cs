using UnityEngine;
using System.Collections;

public class SelectTurret : MonoBehaviour {
	public int TurretCost=20;
	public int MaxLevel=3;
	public int TurretLevel=0;
	public GameObject RangeAura;
	public float SelectedTime=5f;
	public GameObject Base;
	private GameObject[] Auras;
	public Material SelectMaterial;
	public Material MaxLevelMaterial;
	private Material OriginalMaterial;

	public int UpgradeCost=20;

	// Use this for initialization
	void Start () {
		SelectMaterial=(Material)Resources.Load("Materials/SelectedTurret");
		OriginalMaterial = Base.GetComponent<Renderer> ().material;
	} 
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown(){
		if(CurrentTurret.myCurrentTurret != this.gameObject){

			if(GlobalVariables.DestroyTurret==false){
				GlobalVariables.CurrentTurretLevel=TurretLevel;
				CurrentTurret.myCurrentTurret = this.gameObject;
				DisableAura();
				Base.GetComponent<Renderer> ().material = SelectMaterial;
				RangeAura.GetComponent<Renderer>().enabled=true;
				Invoke ("DisableAura",SelectedTime);
			}else{
				DestroyTurret();
			}


		}else{ 
			CancelInvoke("DisableAura");
			DisableAura();
			CurrentTurret.myCurrentTurret =null;
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
	public void UpgradeTurret(){
		GlobalVariables.UpgradeCost = UpgradeCost;
		if (GlobalVariables.Money >= GlobalVariables.UpgradeCost && GlobalVariables.CurrentTurret != null && TurretLevel<MaxLevel) {
			GlobalVariables.Money=GlobalVariables.Money-GlobalVariables.UpgradeCost;
			Base.SendMessage("ChangeBulletDamage");
			TurretLevel++;
			if(TurretLevel>=MaxLevel){
				OriginalMaterial=MaxLevelMaterial;
				//Base.GetComponent<Renderer> ().material = OriginalMaterial;
			}
		}
	}
}
 