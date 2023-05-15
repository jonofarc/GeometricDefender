using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class StatusEffect : MonoBehaviour
{
    public string[] BulletStatusEffect;
    public bool statusSent = false;
    private List<string> fileLines;
    public List<string> statusProperties;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {


        AddStatus(collision.gameObject);


    }

    public void AddStatus(GameObject Creep)
    {

        if (Creep.gameObject.tag == GlobalVariables.CreepTag && statusSent == false)
        {

            for (int i = 0; i <= BulletStatusEffect.Length - 1; i++)
            {

                statusProperties = BulletStatusEffect[i].Split(',').ToList();

                Creep.SendMessage(statusProperties[0], float.Parse(statusProperties[1]), SendMessageOptions.DontRequireReceiver);
            }
            statusSent = true;

        }
    }

}
