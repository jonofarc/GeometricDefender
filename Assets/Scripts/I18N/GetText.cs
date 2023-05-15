using UnityEngine;
using UnityEngine.UI;

public class GetText : MonoBehaviour
{
    public string TranslationName;//here we assign the tag to search for its translation
                                  // Use this for initialization
    void Awake()
    {
        setText();
        //Invoke ("setText",0.2f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void setText()
    {
        Text myText = gameObject.GetComponent<Text>();
        myText.text = LocalizationText.GetText(TranslationName);
    }
}
