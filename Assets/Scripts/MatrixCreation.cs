using UnityEngine;
using System.Collections;

public class MatrixCreation : MonoBehaviour {
	public GameObject BuildBase;
	public int MatixHeight=3;
	public int MatixWidth=3;
	private GameObject clone;
	// Use this for initialization
	void Start () {
		BuildBase.SetActive(true);
		


		for (int posicionX=0; posicionX<MatixWidth;posicionX++){

			for(int posicionZ=0; posicionZ<MatixHeight;posicionZ++){

				clone = Instantiate(BuildBase, BuildBase.transform.position, BuildBase.transform.rotation) as GameObject;
				clone.gameObject.SetActive(true);

				clone.transform.parent=this.transform;
				clone.transform.localScale=BuildBase.transform.localScale; 
				clone.transform.localPosition=new Vector3(posicionX,BuildBase.transform.localPosition.y,posicionZ);
			}
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
