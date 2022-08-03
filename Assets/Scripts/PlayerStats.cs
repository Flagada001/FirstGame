using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

public class PlayerStats : CombatStats
{
    // // Spare hp
    // private float maxKi;
    // public float MaxKi { get => maxKi; }

    // // Melee combat damage
    // private float maxPhysical;
    // public float MaxPhysical { get => maxPhysical; }

    // // Movement speed, maybe Dodge rating
    // private float maxSpeed;
    // public float MaxSpeed { get => maxSpeed; }

    // // Special move damage
    // private float maxEnergy;
    // public float MaxEnergy { get => maxEnergy; }

    // // Return Total value of all Max stat
    // public float MaxTotal { get => maxPhysical + maxSpeed + maxEnergy + maxKi; }


    // // All Stats will get weaker as you are damaged
    // private float currentKi;
    // public float CurrentKi { get => currentKi; }

    // private float currentPhysical;
    // public float CurrentPhysical { get => currentPhysical; }

    // private float currentEnergy;
    // public float CurrentEnergy { get => currentEnergy; }

    // private float currentSpeed;
    // public float CurrentSpeed { get => currentSpeed; }

    // public float CurrentTotal { get => CurrentPhysical + CurrentSpeed + CurrentEnergy + CurrentKi; }


    // Start is called before the first frame update
    void Start()
    {
        MaxKi = 1;
        MaxPhysical = 1;
        MaxSpeed = 1;
        MaxEnergy = 1;
        CurrentKi = MaxKi;
        CurrentPhysical = MaxPhysical;
        CurrentSpeed = MaxSpeed;
        CurrentEnergy = MaxEnergy;
    }
}
