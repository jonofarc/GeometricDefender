using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    public Text fpsDisplay;
    float fpsTotal = 0f;
    float deltaTime = 0.0f;


    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        float fps = 1.0f / deltaTime;

        float msec = Time.unscaledDeltaTime * 1000.0f;
        if (fpsDisplay != null)
        {
            fpsDisplay.text = fps.ToString("n0") + " FPS";
        }
        // string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);


    }
}