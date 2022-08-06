using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    private PlayerStats characterStat;
    private PlayerActionManager actionManager;


    // Start is called before the first frame update
    void Start()
    {
        characterStat = gameObject.GetComponent<PlayerStats>();
        actionManager = gameObject.GetComponent<PlayerActionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO : be able to select the right mouse button skill in game
        // Spawn a Ki Blast Projectile in Hand on Right mouse
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            actionManager.KiBlastPressed();
        }

        // Throw Ki Blast Projectile in Hand when releasing the Right Mouse
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            actionManager.KiBlastReleased();
        }

        // 
        if (Input.GetKey(KeyCode.Mouse1))
        {
            float overcharge = characterStat.OverchargeEnergy(characterStat.MaxEnergy, characterStat.CurrentEnergy);
            characterStat.CurrentEnergy += overcharge;
            characterStat.GainMoreEnergy(overcharge);
        }
    }
}
