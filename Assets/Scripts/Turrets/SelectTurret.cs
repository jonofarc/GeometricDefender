using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

			refreshText();



			if(GlobalVariables.DestroyTurret==false){
				GlobalVariables.CurrentTurretLevel=TurretLevel;
				CurrentTurret.myCurrentTurret = this.gameObject;
				DisableAura();
				UpdateMaterial (true);
				RangeAura.GetComponent<Renderer>().enabled=true;
				Invoke ("DisableAura",SelectedTime);
			}else{
				DestroyTurret(0.5f);

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
	public void DestroyTurret(float MoneyBackPercentage ){
		if (GlobalVariables.GameStarted == true) {
			
			GlobalVariables.Money = GlobalVariables.Money + ((int)(TurretCost * MoneyBackPercentage));
			this.transform.parent.gameObject.GetComponent<BoxCollider> ().enabled = true;
			Destroy (this.gameObject);

		} else {

			GlobalVariables.Money = GlobalVariables.Money + ((int)(TurretCost));
			this.transform.parent.gameObject.GetComponent<BoxCollider> ().enabled = true;
			Destroy (this.gameObject);		
		
		}

	}
	public int getTurretCost(){
	
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
			UpdateMaterial (false);
			UpgradeCost =(int)(UpgradeCost * 2.5f);
			refreshText();
		}
	}

	public void refreshText(){

		GameObject info=GameObject.FindWithTag("Info");

		DestroyBullet[] Bullets=this.gameObject.GetComponentsInChildren<DestroyBullet>(true);
		if (TurretLevel >= MaxLevel) {
			info.GetComponent<Text>().text=LocalizationText.GetText("MaximiunLevel")+"\n"+LocalizationText.GetText("Damage")+" : "+Bullets[0].BulletDamage.ToString();
		} else {
			info.GetComponent<Text>().text=LocalizationText.GetText("UpgradeCost")+": "+UpgradeCost+"\n "+LocalizationText.GetText("DamageIncrease")+": "+(Bullets[0].BulletDamage*Bullets[0].BulletDamageIncrease).ToString();
		}
		//GameObject.Find ("Upgrade").transform.GetChild (0).SendMessage ("GetTurretLevel");
		//thisText.text = CurrentTurret.myCurrentTurret.GetComponent<SelectTurret> ().TurretLevel.ToString();

	}
	public void UpdateMaterial(bool TurretSelected){
		Debug.Log (TurretLevel);
		if(TurretLevel>=MaxLevel){
			OriginalMaterial=MaxLevelMaterial;
			Base.GetComponent<Renderer> ().material = OriginalMaterial;
			//Base.GetComponent<Renderer> ().material = OriginalMaterial;
		}
		if(TurretSelected){
			Base.GetComponent<Renderer> ().material = SelectMaterial;
		}

	}
}
 