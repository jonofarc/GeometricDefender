using UnityEngine;

public class ReturnToMainMenu : MonoBehaviour
{
    private bool m_Levelloaded;


    public void Start()
    {

    }


    public void GoBackToMainMenu()
    {
        Debug.Log("going back to main menu");
        Application.LoadLevel("MainMenu");
    }
}
