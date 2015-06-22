using UnityEngine;
using System.Collections;

public class ReachEnd : MonoBehaviour {

	public int HP=20;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject);
	//	Debug.Log ("-1 hp");
		HP--;
	//	Debug.Log ("Remaining health "+HP);
	}
}
