﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectTurret : MonoBehaviour
{
	public int TurretCost = 20;
	public int MaxLevel = 3;
	public int TurretLevel = 0;
	public GameObject RangeAura;
	public float SelectedTime = 5f;
	public GameObject Base;
	private GameObject[] Auras;
	public Material SelectMaterial;
	public Material MaxLevelMaterial;
	private Material OriginalMaterial;

	public int UpgradeCost = 20;

	// Use this for initialization
	void Start ()
	{
		SelectMaterial = (Material)Resources.Load ("Materials/SelectedTurret");
		OriginalMaterial = Base.GetComponent<Renderer> ().sharedMaterial;
	}
	
	// Update is called once per frame
	void Update ()
	{


	}

	void OnMouseDown ()
	{

		DragSelect ();
		
	}

	void resetMaterial ()
	{
		Base.GetComponent<Renderer> ().material = OriginalMaterial;
	}

	void getCost ()
	{
		GlobalVariables.TurretCost = TurretCost;
	}

	public void DestroyTurret (float MoneyBackPercentage)
	{

		GameObject mainCamera = GameObject.FindGameObjectWithTag (GlobalVariables.MainCameraTag);
		mainCamera.SendMessage ("PlaySound","Sound/RemoveTurretSoundEffect");

		GlobalVariables.Money = GlobalVariables.Money + ((int)(TurretCost * MoneyBackPercentage));
		this.transform.parent.gameObject.GetComponent<BoxCollider> ().enabled = true;
		foreach (Transform child in this.transform) {
			GameObject.Destroy (child.gameObject);
		}
		GameObject PathChecker = GameObject.FindGameObjectWithTag (GlobalVariables.CreepPathCheckerTag);
		if (PathChecker != null) {
			PathChecker.SendMessage ("ReSetCreepsPath", SendMessageOptions.DontRequireReceiver);
			Destroy (this.gameObject);
		}

	}

	public int getTurretCost ()
	{
	
		return TurretCost;
	}

	public void DisableAura ()
	{
		Auras = GameObject.FindGameObjectsWithTag ("RangeAura");
		foreach (GameObject Aura in Auras) {
			Aura.GetComponent<Renderer> ().enabled = false;
			Aura.SendMessageUpwards ("resetMaterial", SendMessageOptions.DontRequireReceiver);
		}
	
	}

	public void UpgradeTurret ()
	{
		GlobalVariables.UpgradeCost = UpgradeCost;
		if (GlobalVariables.Money >= GlobalVariables.UpgradeCost && GlobalVariables.CurrentTurret != null && TurretLevel < MaxLevel) {
			GlobalVariables.Money = GlobalVariables.Money - GlobalVariables.UpgradeCost;
			Base.SendMessage ("ChangeBulletDamage");
			TurretLevel++;
			UpdateMaterial (false);
			UpgradeCost = (int)(UpgradeCost * 2.5f);
			refreshText ();
		}
	}

	public void refreshText ()
	{

		GameObject info = GameObject.FindWithTag ("Info");

		DestroyBullet[] Bullets = this.gameObject.GetComponentsInChildren<DestroyBullet> (true);
		if (TurretLevel >= MaxLevel) {
			info.GetComponent<Text> ().text = LocalizationText.GetText ("MaximiunLevel") + "\n" + LocalizationText.GetText ("Damage") + " : " + Bullets [0].BulletDamage.ToString ();
		} else {
			info.GetComponent<Text> ().text = LocalizationText.GetText ("UpgradeCost") + ": " + UpgradeCost + "\n " + LocalizationText.GetText ("DamageIncrease") + ": " + (Bullets [0].BulletDamage * Bullets [0].BulletDamageIncrease).ToString ();
		}
		//GameObject.Find ("Upgrade").transform.GetChild (0).SendMessage ("GetTurretLevel");
		//thisText.text = CurrentTurret.myCurrentTurret.GetComponent<SelectTurret> ().TurretLevel.ToString();

	}

	public void UpdateMaterial (bool TurretSelected)
	{

		if (TurretLevel >= MaxLevel) {
			OriginalMaterial = MaxLevelMaterial;
			Base.GetComponent<Renderer> ().material = OriginalMaterial;
			//Base.GetComponent<Renderer> ().material = OriginalMaterial;
		}
		if (TurretSelected) {
			Base.GetComponent<Renderer> ().material = SelectMaterial;
		}

	}

	public void DragSelect ()
	{
		Debug.Log ("DRAGSELECT ON SELECT TURRET");
		if (CurrentTurret.myCurrentTurret != this.gameObject) {

			refreshText ();
			GlobalVariables.CurrentTurretLevel = TurretLevel;
			//Check if this is a turret before asigning current turret
			if (this.gameObject.tag == GlobalVariables.TurretTag) {
				
				CurrentTurret.myCurrentTurret = this.gameObject;	
			}
			DisableAura ();
			UpdateMaterial (true);
			RangeAura.GetComponent<Renderer> ().enabled = true;
			//Invoke ("DisableAura",SelectedTime);





		}




	}
}
 