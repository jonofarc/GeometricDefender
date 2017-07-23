using UnityEngine;
using System.Collections;

public class StatusChange : MonoBehaviour {

	private float NormalSpeed=0;
    public float FreezeStatusTime = 3.0f;
	public float DoTStatusTime = 3.0f;
	private float CurrentDoTDamage = 3.0f;
    private Material FreezeMaterial;
	private Material ToxicMaterial;
    private Material OriginalMaterial;
	private int DoTTicks = 0;
	private float DoTTicksTime = 1.0f;
    // Use this for initialization
    void Start () {
		NormalSpeed = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ().speed;
        FreezeMaterial = (Material)Resources.Load("Materials/FreezeMaterial");
		ToxicMaterial = (Material)Resources.Load("Materials/ToxicMaterial");
		OriginalMaterial = this.GetComponent<Renderer>().sharedMaterial;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Freeze(float SlowAmount){

		Debug.Log ("slowAmount: "+ SlowAmount);
		this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ().speed = NormalSpeed* SlowAmount ;
		Debug.Log ("normal speed:"+NormalSpeed+ "  CurrentSpeed="+this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ().speed);
        this.GetComponent<Renderer>().material = FreezeMaterial;
        CancelInvoke("UnFreeze");
		Invoke("UnFreeze",FreezeStatusTime);

	}
    public void UnFreeze() { 

        this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = NormalSpeed;
        this.GetComponent<Renderer>().material = OriginalMaterial;
    }
	public void Poison(float DoTDamage){
		Debug.Log ("Entering Poison");

		//This is to prevent Poison stacking
		CancelInvoke ("ApplyDamage");

		CurrentDoTDamage = DoTDamage;
		this.GetComponent<Renderer>().material = ToxicMaterial;
		InvokeRepeating ("ApplyDamage",DoTTicksTime,DoTTicksTime);

		
	}
	public void ApplyDamage(){
		Debug.Log ("PoisonDPS: " +CurrentDoTDamage+" Times:"+DoTTicks);
		if (DoTTicks < 3) {
			CreepLife myCreepLifeScript = gameObject.GetComponent<CreepLife> ();
			myCreepLifeScript.takeDamage (CurrentDoTDamage);
			DoTTicks++;

		} else {
			this.GetComponent<Renderer>().material = OriginalMaterial;
			CancelInvoke ("ApplyDamage");
		}
	}

}
