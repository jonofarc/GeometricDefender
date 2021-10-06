using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    public Text fpsDisplay;
    float fpsTotal = 0f;


    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        float fps = 1 / Time.unscaledDeltaTime;

        float msec = Time.unscaledDeltaTime * 1000.0f;
        
       // string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        fpsDisplay.text = fps.ToString("f1")+" FPS";

    }
}