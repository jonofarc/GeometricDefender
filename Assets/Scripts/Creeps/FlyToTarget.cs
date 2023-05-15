using UnityEngine;

public class FlyToTarget : MonoBehaviour
{
    public GameObject Target;
    public float FlyingSpeed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        Vector3 CreepTargetCenter = Target.transform.position;
        CreepTargetCenter.y = this.transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, CreepTargetCenter, FlyingSpeed * Time.deltaTime);
    }
}
