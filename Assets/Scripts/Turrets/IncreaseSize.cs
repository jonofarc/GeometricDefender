using UnityEngine;
using System.Collections;

public class IncreaseSize : MonoBehaviour {

	public Vector3 StartSize;
	public Vector3 FinishSize;
	public bool StopOnCurrentSize=true;
	private Vector3 CurrentSize;
	public float TransitionTime=1;

	// Use this for initialization
	void Start () {

		if(StopOnCurrentSize){

			FinishSize.x=this.transform.localScale.x;
			FinishSize.y=this.transform.localScale.y;
			FinishSize.z=this.transform.localScale.z;
		}
		CurrentSize = new Vector3 (this.transform.localScale.x,this.transform.localScale.y,this.transform.localScale.z);
		this.transform.localScale=new Vector3(StartSize.x,StartSize.y,StartSize.z);

	}
	
	// Update is called once per frame
	void Update () {


		if (this.transform.localScale.x >= FinishSize.x && this.transform.localScale.y >= FinishSize.y && this.transform.localScale.z >= FinishSize.z) {
			 
		} else {
			this.transform.localScale = new Vector3 (this.transform.localScale.x +1*((Time.deltaTime * (FinishSize.x-StartSize.x))/TransitionTime),
			                                         this.transform.localScale.y +1*((Time.deltaTime * (FinishSize.y-StartSize.y))/TransitionTime), 
			                                         this.transform.localScale.z +1*((Time.deltaTime * (FinishSize.z-StartSize.z))/TransitionTime));

		}


	}
}
