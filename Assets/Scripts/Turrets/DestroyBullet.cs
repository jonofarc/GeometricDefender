using UnityEngine;
using System.Collections;

public class DestroyBullet : MonoBehaviour {
	public float BulletDamage=10;
	public bool DamageDone=false;
	public float BulletSpeed = 20f;
	public float TimeToDisapear = 2f;
	private GameObject CreepTarget;
	// Use this for initialization
	void Start () {
	
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

		if(other.GetComponent<Renderer>()!=null){
			if(other.GetComponent<Renderer>().enabled){
			
				Destroy(this.gameObject);
			}
		}
	}
	void OnCollisionEnter(Collision collision) {


		if(collision.gameObject.tag=="CreepG" && DamageDone==false){
		
			collision.gameObject.SendMessage("takeDamage",BulletDamage);
			DamageDone = true;
			Destroy(this.gameObject.GetComponent<Collider>());

		}


		Invoke ("AutoDestroy",TimeToDisapear);


	} 
	void AutoDestroy(){
		Destroy(this.gameObject);
	}
}
