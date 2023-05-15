using UnityEngine;

public class FreezeScript : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float FreezePercentage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {

        /*if(other.GetComponent<Renderer>()!=null){
			if(other.GetComponent<Renderer>().enabled){
			
				Destroy(this.gameObject);
			}
		}*/
        //FreezeCreep (other.gameObject); 
    }
    void OnCollisionEnter(Collision collision)
    {


        FreezeCreep(collision.gameObject);


    }
    void FreezeCreep(GameObject Creep)
    {

        if (Creep.gameObject.tag == GlobalVariables.CreepTag)
        {


            Debug.Log(Creep.GetComponent<UnityEngine.AI.NavMeshAgent>().speed);
            Creep.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = (Creep.GetComponent<UnityEngine.AI.NavMeshAgent>().speed * (FreezePercentage / 100));
            Debug.Log(Creep.GetComponent<UnityEngine.AI.NavMeshAgent>().speed);
        }
    }



}
