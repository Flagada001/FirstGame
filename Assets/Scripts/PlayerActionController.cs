using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    private CharacterRPGStats characterStat;
    private PlayerActionManager actionManager;


    // Start is called before the first frame update
    void Start()
    {
        characterStat = gameObject.GetComponent<CharacterRPGStats>();
        actionManager = gameObject.GetComponent<PlayerActionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) { return; }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            actionManager.KiBlastPressed();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            actionManager.KiBlastReleased();
        }

    }
}
