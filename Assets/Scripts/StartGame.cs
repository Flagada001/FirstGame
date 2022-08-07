using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
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
}
