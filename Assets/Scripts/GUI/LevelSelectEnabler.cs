using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectEnabler : MonoBehaviour
{


    public GameObject[] Levels;
    // Start is called before the first frame update
    void Start()
    {

        Invoke("enableLevels", 0.5f);


    }

    // Update is called once per frame
    void Update()
    {

    }


    void enableLevels()
    {


        for (int i = 1; i < Levels.Length; i++)
        {


            int hp = PlayerPrefs.GetInt("GeometricDefenseLvl" + (i - 1) + "HP");

            if (hp > 0)
            {
                Levels[i].SetActive(true);
            }
            else
            {
                Levels[i].SetActive(false);
            }

        }


    }

}
