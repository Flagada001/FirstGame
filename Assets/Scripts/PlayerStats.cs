using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Placehodler for any RPG element and statistic of the main Character

public class PlayerStats : MonoBehaviour
{
    // Spare hp
    private float maxKi;
    public float MaxKi
    {
        get { return maxKi; }
    }

    //melee combat damage
    private float maxPhysical;
    public float MaxPhysical
    {
        get { return maxPhysical; }
    }

    //Movement speed, maybe Dodge rating
    private float maxSpeed;
    public float MaxSpeed
    {
        get { return maxSpeed; }
    }

    //special move damage
    private float maxEnergy;
    public float MaxEnergy
    {
        get { return maxEnergy; }
    }

    //Return Total value of all Max stat
    public float MaxTotal
    {
        get { return maxPhysical + maxSpeed + maxEnergy + maxKi; }
    }

    //All Stats will get weaker as you are damaged
    public float currentKi;
    public float currentPhysical;
    public float currentEnergy;
    private float currentSpeed;
    public float CurrentSpeed
    {
        get { return currentSpeed; }
    }

    // Start is called before the first frame update
    void Start()
    {
        maxKi = 1;
        maxPhysical = 1;
        maxSpeed = 1;
        maxEnergy = 1;
        currentKi = maxKi;
        currentPhysical = maxPhysical;
        currentSpeed = maxSpeed;
        currentEnergy = maxEnergy;
    }

    public void TakeDamage(float damage)
    {
        //Ki, which is the spare hp will always be lost first without getting weaker
        if (damage <= currentKi)
        {
            currentKi -= damage;
        }
        else
        {
            //Spread damage proportionally to all character stats base
            damage -= currentKi;
            currentPhysical -= damage / 3; //Todo instead of dividing by 3, device by the ratio of currentPhysical/(currentPhysical+currentSpeed+currentEnergy)
            currentSpeed -= damage / 3;
            currentEnergy -= damage / 3;
        }
    }

    public float ReturnCurrentTotal()
    {
        return currentPhysical + currentSpeed + currentEnergy + currentKi;
    }

    public float ReturnMaxTotal()
    {
        return maxPhysical + maxSpeed + maxEnergy + maxKi;
    }
    public float ReturnCurrentKi()
    {
        return currentKi;
    }

    public float ReturnMaxKi()
    {
        return maxKi;
    }
    public float ReturnCurrentPhysical()
    {
        return currentPhysical;
    }

    public float ReturnMaxPhysical()
    {
        return maxPhysical;
    }
    public float ReturnCurrentSpeed()
    {
        return currentSpeed;
    }

    public float ReturnMaxSpeed()
    {
        return maxSpeed;
    }
    public float ReturnCurrentEnergy()
    {
        return currentEnergy;
    }

    public float ReturnMaxEnergy()
    {
        return maxEnergy;
    }
}
