using UnityEngine;

public class LimitedInvincibility : MonoBehaviour
{

    public float InvincibilityTime = 0;
    public Material ShieldMaterial;
    private Material OriginalMaterial;
    private float OriginalShield = 0;
    public bool ShieldUsed = false;
    // Use this for initialization
    void Start()
    {
        OriginalMaterial = this.gameObject.GetComponent<Renderer>().sharedMaterial;
        OriginalShield = this.gameObject.GetComponent<CreepLife>().CreepShield;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreepEfect()
    {

        if (ShieldUsed == false)
        {
            ShieldUsed = true;
            SetShield();
            Invoke("DestroyShield", InvincibilityTime);
        }


    }
    public void SetShield()
    {

        this.gameObject.GetComponent<CreepLife>().CreepShield = 9999;
        if (ShieldMaterial != null)
        {
            this.gameObject.GetComponent<Renderer>().material = ShieldMaterial;
        }

    }
    public void DestroyShield()
    {

        this.gameObject.GetComponent<CreepLife>().CreepShield = OriginalShield;
        this.gameObject.GetComponent<Renderer>().material = OriginalMaterial;

    }
}
