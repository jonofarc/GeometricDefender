using UnityEngine;
using System.Collections;

public class GenerateOnclick : MonoBehaviour {
	private GameObject PathCheker;
	private GameObject clone;

	public GameObject[] BuildBase;

	NavMeshObstacle navMeshObstacle;
	Renderer rend;
	// Use this for initialization
	void Start () {

		BuildBase=GameObject.FindGameObjectsWithTag("Turret");
		PathCheker=GameObject.FindGameObjectWithTag("PathCheker");
		rend = GetComponent<Renderer>();
		navMeshObstacle = GetComponent<NavMeshObstacle>();
	
	}
	
	// Update is called once per frame
	void Update () { 

	
	}
	void OnMouseDown(){

		//rend.enabled = true;
		//navMeshObstacle.enabled = true;
	

		clone = Instantiate(BuildBase[0], BuildBase[0].transform.position, BuildBase[0].transform.rotation) as GameObject;
		clone.gameObject.SetActive(true);
		
		clone.transform.parent=this.transform;
		clone.transform.localScale=BuildBase[0].transform.localScale; 
		clone.transform.localPosition = new Vector3 (0,-0.49f,0);

		PathCheker.SendMessage ("SetLatestTurret",clone.gameObject);

	}
	void ReverseTurret(){
		Debug.Log ("Destruyendo");
		rend.enabled = false;
		navMeshObstacle.enabled = false;
	}
}
