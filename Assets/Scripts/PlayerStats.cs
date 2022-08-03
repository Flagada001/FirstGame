using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

public class PlayerStats : CombatStats
{
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
