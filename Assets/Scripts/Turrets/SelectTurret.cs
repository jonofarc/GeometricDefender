using UnityEngine;
using System.Collections;

public class SelectTurret : MonoBehaviour {
	public GameObject RangeAura;
	public GameObject Base;
	public GameObject[] Auras;
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
		CurrentTurret.myCurrentTurret = this.gameObject;
		Auras= GameObject.FindGameObjectsWithTag ("RangeAura");
		foreach (GameObject Aura in Auras) {
			Aura.GetComponent<Renderer>().enabled=false;
			Aura.SendMessageUpwards("resetMaterial");
		}
		Base.GetComponent<Renderer> ().material = SelectMaterial;
		RangeAura.GetComponent<Renderer>().enabled=true;
	
		
	}
	void resetMaterial(){
		Base.GetComponent<Renderer> ().material = OriginalMaterial;
	}
}
 