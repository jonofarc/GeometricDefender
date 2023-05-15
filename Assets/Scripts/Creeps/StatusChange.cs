using UnityEngine;

public class StatusChange : MonoBehaviour
{

    private float NormalSpeed = 0;
    public float FreezeStatusTime = 3.0f;
    public float DoTStatusTime = 3.0f;
    private float CurrentDoTDamage = 3.0f;
    private Material FreezeMaterial;
    private Material ToxicMaterial;
    private Material ToxicFreezeMaterial;
    private Material ElementalAbsorbMaterial;
    private Material OriginalMaterial;
    private int DoTTicks = 0;
    private float DoTTicksTime = 1.0f;

    private bool CreepPoison = false;
    private bool CreepFreeze = false;
    public bool ElementalResistant = false;//resist poison and freeze
    public bool ElementalAbsorvent = false;//gets buff with poion and freeze
    private GameObject CreepRenderGameObject;
    // Use this for initialization
    void Start()
    {
        NormalSpeed = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed;
        FreezeMaterial = (Material)Resources.Load("Materials/FreezeMaterial");
        ToxicMaterial = (Material)Resources.Load("Materials/ToxicMaterial");
        ToxicFreezeMaterial = (Material)Resources.Load("Materials/ToxicFreezeMaterial");
        ElementalAbsorbMaterial = (Material)Resources.Load("Materials/AbsorbentMaterial");
        try
        {
            CreepRenderGameObject = this.transform.gameObject;
            OriginalMaterial = CreepRenderGameObject.GetComponent<Renderer>().sharedMaterial;

        }
        catch
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (this.transform.GetChild(i).tag == GlobalVariables.CreepRenderTag)
                {
                    CreepRenderGameObject = this.transform.GetChild(i).gameObject;
                    OriginalMaterial = CreepRenderGameObject.GetComponent<Renderer>().sharedMaterial;
                }
            }
            //OriginalMaterial = this.GetComponent<Renderer>().sharedMaterial;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Freeze(float SlowAmount)
    {
        if (!ElementalResistant)
        {
            //reverse the efect of freeze
            if (ElementalAbsorvent)
            {
                SlowAmount++;
            }
            Debug.Log("slowAmount: " + SlowAmount);
            this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = NormalSpeed * SlowAmount;
            Debug.Log("normal speed:" + NormalSpeed + "  CurrentSpeed=" + this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed);
            CreepFreeze = true;
            MaterialAdjust();
            CancelInvoke("UnFreeze");
            Invoke("UnFreeze", FreezeStatusTime);
        }


    }
    public void UnFreeze()
    {

        this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = NormalSpeed;

        CreepFreeze = false;
        MaterialAdjust();
    }
    public void Poison(float DoTDamage)
    {
        if (!ElementalResistant)
        {
            Debug.Log("Entering Poison");

            if (ElementalAbsorvent)
            {
                DoTDamage = -1 * DoTDamage;
            }
            //This is to prevent Poison stacking
            CancelInvoke("ApplyDamage");

            CurrentDoTDamage = DoTDamage;

            CreepPoison = true;
            MaterialAdjust();
            InvokeRepeating("ApplyDamage", DoTTicksTime, DoTTicksTime);

        }
    }
    public void ApplyDamage()
    {
        Debug.Log("PoisonDPS: " + CurrentDoTDamage + " Times:" + DoTTicks);
        if (DoTTicks < 3)
        {
            CreepLife myCreepLifeScript = gameObject.GetComponent<CreepLife>();
            myCreepLifeScript.takeDamage(CurrentDoTDamage);
            DoTTicks++;

        }
        else
        {

            CreepPoison = false;
            MaterialAdjust();
            CancelInvoke("ApplyDamage");
        }
    }

    public void MaterialAdjust()
    {

        if (CreepPoison == false && CreepFreeze == false)
        {
            CreepRenderGameObject.GetComponent<Renderer>().material = OriginalMaterial;
        }
        else if (ElementalAbsorvent)
        {
            CreepRenderGameObject.GetComponent<Renderer>().material = ElementalAbsorbMaterial;
        }
        else if (CreepPoison && CreepFreeze)
        {
            CreepRenderGameObject.GetComponent<Renderer>().material = ToxicFreezeMaterial;
        }
        else if (CreepPoison)
        {
            CreepRenderGameObject.GetComponent<Renderer>().material = ToxicMaterial;
        }
        else if (CreepFreeze)
        {
            CreepRenderGameObject.GetComponent<Renderer>().material = FreezeMaterial;
        }
    }

}
