using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControls : MonoBehaviour
{
    public GameObject statTab;

    private CharacterRPGStats characterStat;

    void Start()
    {
        characterStat = (CharacterRPGStats)gameObject.GetComponent(typeof(CharacterRPGStats));
    }

    // Update is called once per frame
    void Update()
    {
        //Open the stats interface
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (statTab.activeSelf)
            {
                statTab.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                statTab.SetActive(true);
                Time.timeScale = 0;
            }
        }


    }
}
