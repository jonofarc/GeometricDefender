using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ContinueScrollSize : MonoBehaviour
{
    public TextAsset CreepSpawnList;
    public GameObject levelGUIButton;
    public float itemSize = 3;
    public float margingTop = 50;
    public float HeightSeparation = 130;
    private List<string> FileLines;


    // Start is called before the first frame update
    void Start()
    {
        readLevelList();
    }

    // Update is called once per frame
    void Update()
    {
    
    }


    public void readLevelList()
    {
        var LevelCount = 0;


        FileLines = CreepSpawnList.text.Split('\n').ToList();

        for (int i = 0; i < FileLines.Count(); i++)
        {

            string Line = FileLines[i];

            if (!Line.Equals(" ") && !Line.StartsWith("#") && !Line.StartsWith("//") && !Line.StartsWith(" ") && Line.Contains("@level"))
            {



                LevelCount++;


            }
            
        }

        
        

        ChangeHeightValue((((LevelCount-9)/3)* itemSize)+margingTop);
        CreateLevelButtons(LevelCount);

    }

    void CreateLevelButtons(int buttonAmount)
    {
        //All of this logic could probably be solved in a better way but at this piont this is the first solution that comes to mind
        int YMultiplier = 0;
        int YIncreaseFlag = 0;
        for (int i =1; i< buttonAmount+1; i++) {
            GameObject duplicatedObject = Instantiate(levelGUIButton);

            // Access RectTransforms for positioning in UI space
            RectTransform parentRectTransform = levelGUIButton.GetComponent<RectTransform>();
            RectTransform childRectTransform = duplicatedObject.GetComponent<RectTransform>();

            // Set the parent of the childElement to be the parentElement
            childRectTransform.SetParent(parentRectTransform.parent, false); // Set the second parameter to true to maintain local position

            duplicatedObject.name = "Level"+i;
            Text textComponent = duplicatedObject.GetComponentInChildren<Text>();
            textComponent.text = i.ToString();


            if (i % 2 == 0 && i % 3  != 0) 
            {
                duplicatedObject.transform.localPosition = new Vector3(parentRectTransform.localPosition.x+325.0f, parentRectTransform.localPosition.y - (YMultiplier * 130), 0f);
            }else if (i % 3 == 0) 
            {
                duplicatedObject.transform.localPosition = new Vector3(parentRectTransform.localPosition.x+650.0f, parentRectTransform.localPosition.y - (YMultiplier * 130), 0f);
            }
            else 
            {
                duplicatedObject.transform.localPosition = new Vector3(parentRectTransform.localPosition.x, parentRectTransform.localPosition.y-(YMultiplier*130), 0f); 
            }

            YIncreaseFlag++;
            if (YIncreaseFlag >= 3) {
                YMultiplier++;
                YIncreaseFlag = 0;
            }

        }
        levelGUIButton.SetActive(false);
    }

    void ChangeHeightValue(float newHeight)
    {
        // Get the RectTransform component attached to the GameObject
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Make sure the RectTransform reference is not null
        if (rectTransform != null)
        {
            // Get the current rect values
            Rect rect = rectTransform.rect;


            // Create a new Rect with the updated height
          
            Rect newRect = new Rect(rect.x, rect.y, 0, newHeight);

            // Apply the modified rect values back to the RectTransform
            rectTransform.sizeDelta = new Vector2(newRect.width, newRect.height);
        }
        else
        {
            Debug.LogError("RectTransform is null. Make sure the script is attached to a GameObject with a RectTransform component.");
        }
    }

}
