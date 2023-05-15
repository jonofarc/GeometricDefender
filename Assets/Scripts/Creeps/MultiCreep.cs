using UnityEngine;

public class MultiCreep : MonoBehaviour
{


    float MiniCreepInterval = 2.0f;
    public bool Execute = true;

    // Use this for initialization
    void Start()
    {
        CancelInvoke("moveMiniCreeps");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreepEfect()
    {

        if (Execute)
        {

            InvokeRepeating("moveMiniCreeps", 0.1f, MiniCreepInterval);
            Execute = false;
        }
        else
        {
            CancelInvoke("moveMiniCreeps");
        }


    }
    void moveMiniCreeps()
    {




        GameObject CreepClone = Instantiate(this.transform.gameObject, transform.position, transform.rotation);


        CreepClone.transform.position = new Vector3(CreepClone.transform.position.x, CreepClone.transform.position.y + this.transform.localScale.y, CreepClone.transform.position.z);


        CreepClone.transform.parent = this.transform.parent;
        CreepClone.transform.localScale = this.transform.localScale / 2;


        CreepClone.name = "Mini " + this.name;
        CreepClone.GetComponent<MultiCreep>().Execute = false;
        CreepClone.GetComponent<MultiCreep>().enabled = false;




    }

}
