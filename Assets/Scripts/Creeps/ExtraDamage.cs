using UnityEngine;

public class ExtraDamage : MonoBehaviour
{
    public int DamageToInflict = 1;
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

        if (other.gameObject.tag == "Finish")
        {
            other.SendMessage("ReciveDamage", DamageToInflict);
        }

    }


}
