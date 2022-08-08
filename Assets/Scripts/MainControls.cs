using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControls : MonoBehaviour
{

    public GameObject statTab;
    private PlayerStats characterStat;

    void Start()
    {
        characterStat = gameObject.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("CurrentScoreText").GetComponent<TMPro.TextMeshProUGUI>().text = string.Format("Score : {0:f0}", characterStat.Score);

        if (GameObject.Find("GameStartMenu") != null) { return; }

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            characterStat.IsDead = true;
        }


    }
}
