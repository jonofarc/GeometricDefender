using UnityEngine;
using System.Collections;

public class LookAtObject : MonoBehaviour {

    private GameObject myObject;
	// Use this for initialization
	void Start () {

        myObject = GameObject.FindGameObjectWithTag("MainCamera");
        transform.LookAt(myObject.transform);



    }
	
	// Update is called once per frame
	void Update () {


       
    }
}
