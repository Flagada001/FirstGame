using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        GameObject.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>().text = string.Format("Previous Score : {0:f0}", PlayerPrefs.GetFloat("Score"));
        GameObject.Find("BestScoreText").GetComponent<TMPro.TextMeshProUGUI>().text = string.Format("Best Score : {0:f0}", PlayerPrefs.GetFloat("BestScore"));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.time);
    }

    public void BeginTheGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
