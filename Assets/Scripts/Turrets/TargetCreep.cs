using UnityEngine;
using System.Collections;

public class TargetCreep : MonoBehaviour {
	public GameObject[] Creeps; 
	public bool AimAtCreep=true;
	public GameObject ClosestCreep; 
	public string targetTag="CreepG";
	public float turretRange=3;
	public GameObject turretRangeAreaMarker;

	public Transform target;
	private float speed =100f;
	
	//public float BulletSpeed = 20f;
	public bool RequireTarget=true;
	public float ShootingSpeed = 1f;
	private float ShootingSpeedOriginal = 1f;
	public GameObject projectile;
	private GameObject bullet;

	private float myTimeTillNextShoot=0;


	// Use this for initialization
	void Start () {
		ShootingSpeedOriginal = ShootingSpeed;
		SetGameSpeed ();
		turretRangeAreaMarker.gameObject.transform.localScale = new Vector3 (turretRange,0.01f,turretRange);


	}

    // Update is called once per frame

    //	void Update() {
    void FixedUpdate(){
		if (target != null ){
			Vector3 targetDir = target.position - transform.position;
			//aim at the center of the target
			targetDir.y=target.position.y+(target.localScale.y/2);
			float step = speed * Time.deltaTime;


			if(AimAtCreep){
				Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
				Debug.DrawRay(transform.position, newDir, Color.red);
				transform.rotation = Quaternion.LookRotation(newDir);  
			}


			TimeTillNextShoot ();



		}else if (RequireTarget==false){

			TimeTillNextShoot ();
		}

	}

	void TimeTillNextShoot(){
		myTimeTillNextShoot += Time.deltaTime;
		if(myTimeTillNextShoot>ShootingSpeed){
			myTimeTillNextShoot -= ShootingSpeed;
			//Invoke("FireBullet",0.1f);
			FireBullet();
		} 
	} 
	void FireBullet(){

		// Instantiate the projectile at the position and rotation of this transform
		bullet = Instantiate(projectile, projectile.transform.position, projectile.transform.rotation) as GameObject;
		bullet.gameObject.SetActive(true);
		
		//bullet.transform.parent=this.transform;
		//bullet.transform.localScale=projectile.transform.localScale; 
		//bullet.transform.parent = null;

//		Rigidbody bulletRigidBody;
		//bulletRigidBody = bullet.GetComponent<Rigidbody>();
		//
		if(target != null){
			//bulletRigidBody.velocity = (target.transform.position - transform.position).normalized * (BulletSpeed); //add foward force to bullet
			bullet.SendMessage("FireAtCreep",ClosestCreep);
		}


	
	}
	void ChangeBulletDamage(){
//		Debug.Log ("entrando ChangeBulletDamage");
		projectile.SetActive (true);
		projectile.SendMessage ("CancelAutoDestroy");
		projectile.SendMessage ("ChangeDamage");
		projectile.SetActive (false);
	}











	
	void FindClosestPlayer() {

		float distance =  Mathf.Pow(turretRange, 2f);;
		if (target == null) {

			Creeps = GameObject.FindGameObjectsWithTag (targetTag);

			foreach (GameObject Creep in Creeps) {
				Vector3 diff = Creep.transform.position - transform.position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) { 
					ClosestCreep = Creep;
					distance = curDistance;
					target = ClosestCreep.transform;

				}
			} // end foreach
		}//end if target==null
		else { //we check if target is to far away

			Vector3 diff = target.transform.position - transform.position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance > distance) { 

				target = null;

				
			}
		}



	}//end FindClosestPlayer

	public void SetGameSpeed(){
		
		ShootingSpeed = ShootingSpeedOriginal / GlobalVariables.GameSpeed;
		CancelInvoke ("FindClosestPlayer");
		if(RequireTarget){
			FindClosestPlayer ();
			InvokeRepeating("FindClosestPlayer", 0.5f,(0.5f/GlobalVariables.GameSpeed));	
		}
	}
}
