using UnityEngine;
using System.Collections;

public class DestroyBullet : MonoBehaviour {
	public float BulletDamage=10;
	public float BulletDamageIncrease=1; 
	public bool ContinousDamage=false;
	public bool DamageDone=false;
	public float BulletSpeed = 20f;
	public float TimeToDisapear = 2f;
	private GameObject CreepTarget;
	public int TargetsHit=0;
	// Use this for initialization
	void Start () {
		Invoke ("AutoDestroy",TimeToDisapear);
	}
	
	// Update is called once per frame
	void Update () {
		if(DamageDone==false && CreepTarget != null ){
			Vector3 CreepTargetCenter=CreepTarget.transform.position;
			CreepTargetCenter.y=CreepTarget.transform.position.y+(CreepTarget.transform.localScale.y/2);
			transform.position = Vector3.MoveTowards(transform.position, CreepTargetCenter, BulletSpeed*Time.deltaTime);
		}

	}

	void FireAtCreep (GameObject CreepTargetRecived) {
		CreepTarget = CreepTargetRecived;
	}

	void OnTriggerEnter(Collider other) {

		/*if(other.GetComponent<Renderer>()!=null){
			if(other.GetComponent<Renderer>().enabled){
			
				Destroy(this.gameObject);
			}
		}*/
		RecivedCollision (other.gameObject); 
	}
	void OnCollisionEnter(Collision collision) {


		RecivedCollision (collision.gameObject);


	} 
	void RecivedCollision (GameObject collision) {


		
		if(collision.gameObject.tag=="CreepG" && DamageDone==false){
			TargetsHit++;
			collision.gameObject.SendMessage("takeDamage",BulletDamage);
			if(ContinousDamage){
				DamageDone = false;
			}else{
				DamageDone = true;
				Destroy(this.gameObject.GetComponent<Collider>());
			}


			
		}
		 

		//AutoDestroy();



		
	}//end RecivedCollision

	void AutoDestroy(){
//		Debug.Log ("destroying bullet");
		Destroy(this.gameObject);
	}
	void CancelAutoDestroy(){
		CancelInvoke ("AutoDestroy");
	}
	void ChangeDamage(){
		BulletDamage = BulletDamage* BulletDamageIncrease;
	}
}
