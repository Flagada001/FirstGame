using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Placehodler for any RPG element and statistic of the main Character

public class CharacterRPGStats : MonoBehaviour
{

    public float maxKi = 4;
    public float currentKi;
    // public float maxPhsysical = 1;
    // public float currentPhsysical = 1;
    // public float maxSpeed = 1;
    // public float currentSpeed = 1;
    // public float maxEnergy = 1;
    // public float currentEnergy = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentKi = maxKi;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        currentKi -= damage;
    }
}
