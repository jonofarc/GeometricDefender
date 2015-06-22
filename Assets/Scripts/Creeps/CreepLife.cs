using UnityEngine;
using System.Collections;

public class CreepLife : MonoBehaviour {
	public float CreepHP=20;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void takeDamage(float Damage){

		CreepHP -= Damage;
		if(CreepHP<=0){

			Destroy(this.gameObject);

		}
	}
	void IncreaseLife(float increment){
		CreepHP += increment;
	}
}
