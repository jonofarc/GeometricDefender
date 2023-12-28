using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ContinueScrollSize : MonoBehaviour
{
    public TextAsset CreepSpawnList;
    public float itemSize = 3;
    public float margingTop = 50;
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
        Debug.Log("LevelCount" + LevelCount);
        
        
        // Change the height to a new value (e.g., 200 units)
        ChangeHeightValue((((LevelCount-9)/3)* itemSize)+margingTop);
        
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
            //Rect newRect = new Rect(rect.x, rect.y, rect.right, newHeight);
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
