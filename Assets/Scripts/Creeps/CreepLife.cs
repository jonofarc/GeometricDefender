using UnityEngine;
using System.Collections;

public class CreepLife : MonoBehaviour {
	public float CreepHP=20;
	public int LootAmount=2;
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
			MoneyCount.Money=MoneyCount.Money+LootAmount;

		}
	}
	void IncreaseLife(float increment){
		CreepHP *= increment;
	}
}
