using UnityEngine;
using System.Collections;

public class CreepLife : MonoBehaviour {
	public float CreepHP=20;
	private float maxHP;
	public int LootAmount=2;

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

		CreepHP -= Damage;

		if(CreepHP>(maxHP*0.66)){
			HPBar.GetComponent<Renderer>().material=HP66;

		}else if(CreepHP>(maxHP*0.33)){
			HPBar.GetComponent<Renderer>().material=HP33;
			
		}else if(CreepHP>(maxHP*0.01)){
			HPBar.GetComponent<Renderer>().material=HP0;
			
		}

		HPBar.transform.localScale = new Vector3( CreepHP / maxHP,
		                                         HPBar.transform.localScale.y, 
		                                         HPBar.transform.localScale.z);

		HPBar.transform.localPosition = new Vector3 (0.5f-(HPBar.transform.localScale.x/2),
		                                             HPBar.transform.localPosition.y, 
		                                             HPBar.transform.localPosition.z);

		if(CreepHP<=0){
			 
			GlobalVariables.Money=GlobalVariables.Money+LootAmount;
			Destroy(this.gameObject); 


		}

	}
	void IncreaseLife(float increment){
		CreepHP *= increment;
		maxHP = CreepHP;
	}
}
