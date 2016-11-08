using UnityEngine;
using System.Collections;

public class LookAtObject : MonoBehaviour {

    public GameObject myObject;
	// Use this for initialization
	void Start () {

        transform.LookAt(myObject.transform);



    }
	
	// Update is called once per frame
	void Update () {


       
    }
}
