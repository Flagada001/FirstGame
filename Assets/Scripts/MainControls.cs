using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControls : MonoBehaviour
{
    public GameObject statTab;

    private PlayerStats characterStat;

    void Start()
    {
        characterStat = (PlayerStats)gameObject.GetComponent(typeof(PlayerStats));
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
