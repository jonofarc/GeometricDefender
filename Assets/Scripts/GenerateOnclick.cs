using UnityEngine;
using System.Collections;

public class GenerateOnclick : MonoBehaviour {
	private GameObject PathCheker;
	private GameObject clone;






	// Use this for initialization
	void Start () {


		PathCheker=GameObject.FindGameObjectWithTag("PathCheker");

	
	}
	
	// Update is called once per frame
	void Update () { 

	
	}
	void OnMouseDown(){

        setPlaceHolder();
        //PlaceTurret();

    }
    void destroyPlaceHolder() {
        if (GlobalVariables.PlaceHolderTurret != null)
        {
            Destroy(GlobalVariables.PlaceHolderTurret);
        }
    }
    void setPlaceHolder() {

        if (GlobalVariables.CurrentTurret != null)
        {
            GlobalVariables.CurrentTurretPlace = this.gameObject;
            destroyPlaceHolder();
            GameObject MyPlaceHolderTurret = GameObject.FindGameObjectWithTag("PlaceHolderTurret");
            clone = Instantiate(MyPlaceHolderTurret, MyPlaceHolderTurret.transform.position, MyPlaceHolderTurret.transform.rotation) as GameObject;
            clone.gameObject.SetActive(true);

            clone.transform.parent = this.transform;
            clone.transform.localScale = MyPlaceHolderTurret.transform.localScale;
            clone.transform.localPosition = new Vector3(0, -0.49f, 0);
            clone.name = "PlaceHolderTurret";

            GlobalVariables.PlaceHolderTurret = clone;

        }
        else {
            Debug.Log("No turret selected");
        }

    }

    void PlaceTurret() {

        if (GlobalVariables.Money >= GlobalVariables.TurretCost && GlobalVariables.CurrentTurret != null)
        {
            destroyPlaceHolder();
            GlobalVariables.Money = GlobalVariables.Money - GlobalVariables.TurretCost;
            clone = Instantiate(GlobalVariables.CurrentTurret, GlobalVariables.CurrentTurret.transform.position, GlobalVariables.CurrentTurret.transform.rotation) as GameObject;
            clone.gameObject.SetActive(true);

            clone.transform.parent = this.transform;
            clone.transform.localScale = GlobalVariables.CurrentTurret.transform.localScale;
            clone.transform.localPosition = new Vector3(0, -0.49f, 0);
            clone.name = clone.name + (CurrentTurret.TurretNumber.ToString());
            CurrentTurret.TurretNumber++;

            PathCheker.SendMessage("SetLatestTurret", this.gameObject);

            this.gameObject.GetComponent<Collider>().enabled = false;
        }
        else
        {
            Debug.Log("no price set for turret or selected turret not valid");
        }


    }
	void ReverseTurret(){
		Debug.Log ("Destruyendo");
		if(clone != null){
			Destroy (clone);

		}

	}
}
