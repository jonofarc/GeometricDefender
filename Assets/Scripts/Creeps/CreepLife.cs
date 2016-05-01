using UnityEngine;
using System.Collections;

public class CreepLife : MonoBehaviour {
	public float CreepHP=20;
	public float CreepShield=0;
	public int LootAmount=1;
	public bool EffectBeforeDiying=false;
	public bool LootOnEfect=false;
	private bool LootGiven = false;
	public float HPLeftTrigger=0;
	public float maxHP;


	public GameObject HPBar;
	public Material HP66;
	public Material HP33;
	public Material HP0;

	// Use this for initialization
	void Start () {
		maxHP = CreepHP;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void takeDamage(float Damage){
		if(Damage>CreepShield){
			CreepHP -= Damage;
		}


		if(CreepHP>(maxHP*0.66)){
			HPBar.GetComponent<Renderer>().material=HP66;

		}else if(CreepHP>(maxHP*0.33)){
			HPBar.GetComponent<Renderer>().material=HP33;
			
		}else if(CreepHP>(maxHP*0.01)){
			HPBar.GetComponent<Renderer>().material=HP0;
			
		}
		if((CreepHP / maxHP)>=0 && (CreepHP / maxHP)<=1){


			HPBar.transform.localScale = new Vector3( CreepHP / maxHP,
			                                         HPBar.transform.localScale.y, 
			                                         HPBar.transform.localScale.z);
			
			HPBar.transform.localPosition = new Vector3 (0.5f-(HPBar.transform.localScale.x/2),
			                                             HPBar.transform.localPosition.y, 
			                                             HPBar.transform.localPosition.z);

		}




		if(CreepHP<=0){
			if(LootGiven==false){
				GlobalVariables.Money=GlobalVariables.Money+LootAmount;
				LootGiven = true;
			}

			Destroy(this.gameObject); 


		}

//		Debug.Log (maxHP *(HPLeftTrigger/100));
		if(EffectBeforeDiying && CreepHP<=(maxHP *HPLeftTrigger)){
			if(LootOnEfect && LootGiven==false){
				GlobalVariables.Money=GlobalVariables.Money+LootAmount;
				LootGiven=true;
			}
			this.SendMessage("CreepEfect");
		}


	}
	void IncreaseLife(float increment){
		CreepHP *= increment;
		maxHP = CreepHP;
	}
	void OnTriggerEnter(Collider other) {

		if(other.gameObject.tag=="Bullet"){

			other.SendMessage ("RecivedCollision",this.gameObject);
		}

	}
}
