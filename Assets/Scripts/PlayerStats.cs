using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

public class PlayerStats : CombatStats
{
    private float countSecond = 0;

    // Start is called before the first frame update
    void Start()
    {
        MaxKi = 1;
        MaxPhysical = 1;
        MaxSpeed = 5;
        MaxEnergy = 1;
        CurrentKi = MaxKi;
        CurrentPhysical = MaxPhysical;
        CurrentSpeed = MaxSpeed;
        CurrentEnergy = MaxEnergy;
    }

    void Update()
    {
        countSecond += Time.deltaTime;
        if (countSecond > 1) { countSecond = 0; ApplyHealing(0.2f); }
    }

    public override void ApplyHealing(float healing)
    {
        float initalCurrentTotal = CurrentTotal;
        base.ApplyHealing(healing);
        GainMoreKi(CurrentTotal - initalCurrentTotal);
    }

    private void GainMoreKi(float gains)
    {
        MaxKi += gains / 100;
        CurrentKi += gains / 100;
    }
}
