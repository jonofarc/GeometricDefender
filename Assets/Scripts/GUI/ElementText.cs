using UnityEngine;
using UnityEngine.UI;

public class ElementText : MonoBehaviour
{

    public string LocalizationIdentifier;
    // Use this for initialization
    void Start()
    {
        this.GetComponent<Text>().text = LocalizationText.GetText(LocalizationIdentifier);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
