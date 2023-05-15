using UnityEngine;
using UnityEngine.UI;

public class SelectedTurretLevel : MonoBehaviour
{
    private Text thisText;

    // Use this for initialization
    void Start()
    {
        thisText = this.GetComponent<Text>();
        InvokeRepeating("GetTurretLevel", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GetTurretLevel()
    {

        if (CurrentTurret.myCurrentTurret != null)
        {
            thisText.text = CurrentTurret.myCurrentTurret.GetComponent<SelectTurret>().TurretLevel.ToString();
        }

    }
}
