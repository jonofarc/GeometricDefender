using UnityEngine;
using System.Collections;

public class SelectTurret : MonoBehaviour {
	public GameObject RangeAura;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown(){
		RangeAura.GetComponent<Renderer>().enabled=true;
	
		
	}
}
