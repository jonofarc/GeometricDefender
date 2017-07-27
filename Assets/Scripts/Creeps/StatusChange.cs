using UnityEngine;
using System.Collections;

public class StatusChange : MonoBehaviour {

	private float NormalSpeed=0;
    public float FreezeStatusTime = 3.0f;
	public float DoTStatusTime = 3.0f;
	private float CurrentDoTDamage = 3.0f;
    private Material FreezeMaterial;
	private Material ToxicMaterial;
	private Material ToxicFreezeMaterial;
    private Material OriginalMaterial;
	private int DoTTicks = 0;
	private float DoTTicksTime = 1.0f;

	private bool CreepPoison=false;
	private bool CreepFreeze=false;
    // Use this for initialization
    void Start () {
		NormalSpeed = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ().speed;
        FreezeMaterial = (Material)Resources.Load("Materials/FreezeMaterial");
		ToxicMaterial = (Material)Resources.Load("Materials/ToxicMaterial");
		ToxicFreezeMaterial = (Material)Resources.Load("Materials/ToxicFreezeMaterial");
		OriginalMaterial = this.GetComponent<Renderer>().sharedMaterial;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Freeze(float SlowAmount){

		Debug.Log ("slowAmount: "+ SlowAmount);
		this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ().speed = NormalSpeed* SlowAmount ;
		Debug.Log ("normal speed:"+NormalSpeed+ "  CurrentSpeed="+this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ().speed);
		CreepFreeze = true;
		MaterialAdjust ();
        CancelInvoke("UnFreeze");
		Invoke("UnFreeze",FreezeStatusTime);

	}
    public void UnFreeze() { 

        this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = NormalSpeed;
      
		CreepFreeze = false;
		MaterialAdjust ();
    }
	public void Poison(float DoTDamage){
		Debug.Log ("Entering Poison");

		//This is to prevent Poison stacking
		CancelInvoke ("ApplyDamage");

		CurrentDoTDamage = DoTDamage;

		CreepPoison = true;
		MaterialAdjust ();
		InvokeRepeating ("ApplyDamage",DoTTicksTime,DoTTicksTime);

		
	}
	public void ApplyDamage(){
		Debug.Log ("PoisonDPS: " +CurrentDoTDamage+" Times:"+DoTTicks);
		if (DoTTicks < 3) {
			CreepLife myCreepLifeScript = gameObject.GetComponent<CreepLife> ();
			myCreepLifeScript.takeDamage (CurrentDoTDamage);
			DoTTicks++;

		} else {
			
			CreepPoison = false;
			MaterialAdjust ();
			CancelInvoke ("ApplyDamage");
		}
	}

	public void MaterialAdjust(){

		if (CreepPoison == false && CreepFreeze == false) {
			this.GetComponent<Renderer> ().material = OriginalMaterial;
		} else if (CreepPoison && CreepFreeze) {
			this.GetComponent<Renderer> ().material = ToxicFreezeMaterial;
		} else if (CreepPoison) {
			this.GetComponent<Renderer> ().material = ToxicMaterial;
		} else if (CreepFreeze) {
			this.GetComponent<Renderer> ().material = FreezeMaterial;
		}
	}

}
