using UnityEngine;
using System.Collections;

public class StatusChange : MonoBehaviour {

	private float NormalSpeed=0;
    public float StatusTime = 3.0f;
    private Material FreezeMaterial;
    private Material OriginalMaterial;
    // Use this for initialization
    void Start () {
		NormalSpeed = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ().speed;
        FreezeMaterial = (Material)Resources.Load("Materials/FreezeMaterial");
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
        Invoke("UnFreeze",StatusTime);

	}
    public void UnFreeze() { 

        this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = NormalSpeed;
        this.GetComponent<Renderer>().material = OriginalMaterial;
    }
	public void Poison(float PoisonDPS){
		
		Debug.Log ("PoisonDPS: " +PoisonDPS);
		
	}
}
