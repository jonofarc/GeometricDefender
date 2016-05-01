using UnityEngine;
using System.Collections;

public class StatusChange : MonoBehaviour {

	private float NormalSpeed=0;
	// Use this for initialization
	void Start () {
		NormalSpeed = this.gameObject.GetComponent<NavMeshAgent> ().speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Freeze(float SlowAmount){

		Debug.Log ("slowAmount: "+ SlowAmount);
		this.gameObject.GetComponent<NavMeshAgent> ().speed = NormalSpeed* SlowAmount ;
		Debug.Log ("normal speed:"+NormalSpeed+ "  CurrentSpeed="+this.gameObject.GetComponent<NavMeshAgent> ().speed);


	}
	public void Poison(float PoisonDPS){
		
		Debug.Log ("PoisonDPS: " +PoisonDPS);
		
	}
}
