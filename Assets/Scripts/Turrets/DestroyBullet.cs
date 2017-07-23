using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DestroyBullet : MonoBehaviour {
	public float BulletDamage=10;
	public float BulletDamageIncrease=1; 
	public bool ContinousDamage=false;
	public bool DamageDone=false;
	public float BulletSpeed = 20f;
	public float TimeToDisapear = 2f;
	private GameObject CreepTarget;
	// Use this for initialization
	void Start () {
		
		//Debug.Log (GlobalVariables.CurrentBullets);
		if (GlobalVariables.CurrentBullets < GlobalVariables.MaximunBullets || ContinousDamage == true) {
			Invoke ("AutoDestroy", TimeToDisapear);
			if(ContinousDamage == false){
				GlobalVariables.CurrentBullets++;
			}
		} 
	}

	// Update is called once per frame
	//void Update () {
	void FixedUpdate(){
		if (DamageDone==false && CreepTarget != null ){
			Vector3 CreepTargetCenter=CreepTarget.transform.position;
			CreepTargetCenter.y=CreepTarget.transform.position.y+(CreepTarget.transform.localScale.y/2);
			transform.position = Vector3.MoveTowards(transform.position, CreepTargetCenter, (BulletSpeed*Time.deltaTime)*GlobalVariables.GameSpeed);
		}

	}

	void FireAtCreep (GameObject CreepTargetRecived) {
		CreepTarget = CreepTargetRecived;
	}


	void OnTriggerEnter(Collider other) {

	

		RecivedCollision (other.gameObject); 
	}

	void OnCollisionEnter(Collision collision) {


		RecivedCollision (collision.gameObject);


	} 
	void RecivedCollision (GameObject collision) {



		if(collision.gameObject.tag=="CreepG" && DamageDone==false){
			collision.gameObject.SendMessage("takeDamage",BulletDamage);
			if(ContinousDamage){
				DamageDone = false;
			}else{
				DamageDone = true;
				Destroy(this.gameObject.GetComponent<Collider>());
				if (GlobalVariables.CurrentBullets < 40) {
					Destroy (this.gameObject.GetComponent<Collider> ());
				} else {
					
					AutoDestroy ();
				}
			}



		}


		//AutoDestroy();




	}//end RecivedCollision

	void AutoDestroy(){
		//		Debug.Log ("destroying bullet");
		if(ContinousDamage == false){
			GlobalVariables.CurrentBullets--;
		}
		Destroy(this.gameObject);
	}
	void CancelAutoDestroy(){
		CancelInvoke ("AutoDestroy");
	}
	void ChangeDamage(){
		BulletDamage = BulletDamage* BulletDamageIncrease;

		StatusEffect myStatusEffect = this.gameObject.GetComponent<StatusEffect> ();
		 List<string> statusProperties;
		for(int i=0; i<=myStatusEffect.BulletStatusEffect.Length-1; i++){

			statusProperties = myStatusEffect.BulletStatusEffect[i].Split(',').ToList();

			// ugly hack to increase posion damage should remake everything regarding status efect
			if(statusProperties[0]=="Poison"){
				statusProperties [1] = (int.Parse (statusProperties [1]) * BulletDamageIncrease).ToString ();

				myStatusEffect.BulletStatusEffect [i] = statusProperties [0] + "," + statusProperties [1];
				Debug.Log (myStatusEffect.BulletStatusEffect [i]);
			}


		}
	}
}
