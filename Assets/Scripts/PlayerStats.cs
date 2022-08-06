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
        MaxSpeed = 1;
        MaxEnergy = 1;
        CurrentKi = MaxKi;
        CurrentPhysical = MaxPhysical;
        CurrentSpeed = MaxSpeed;
        CurrentEnergy = MaxEnergy;
    }

    void Update()
    {
        ApplyHealing(0.2f * Time.deltaTime);
    }

    public override void ApplyHealing(float healing)
    {
        float initalCurrentTotal = CurrentTotal;
        base.ApplyHealing(healing);
        GainMoreKi(CurrentTotal - initalCurrentTotal);
        // Debug.Log("Healed" + (CurrentTotal - initalCurrentTotal));
    }

    // Ki increase by healing at a rate of 1 new Ki for 100 ki Healed
    private void GainMoreKi(float gains)
    {
        MaxKi += gains / 100;
        CurrentKi += gains / 100;
    }

    // Energy Increased by overcharging
    public void GainMoreEnergy(float gains)
    {
        MaxEnergy += gains / 100;
        CurrentEnergy += gains / 100;
    }

    // Physical Increased by overcharging
    private void GainMorePhysical(float gains)
    {
        MaxPhysical += gains;
        CurrentPhysical += gains;
    }

    // Speed Increased by overcharging
    private void GainMoreSpeed(float gains)
    {
        MaxSpeed += gains;
        CurrentSpeed += gains;
    }

    public float OverchargeEnergy(float maxStat, float currentStat)
    {
        // TODO : Prevent other 2 Stat from droping bellow a minimum Value
        // TODO : Reduce other stats to raise Energy, Ki first then other 2 Stat proportionally

        // Calculate how much Stat I want to transfer By second  
        // If Energy is equal to all other stat, double value in 10 second
        float overchargeBySecond = Time.deltaTime * CurrentTotal / CurrentEnergy / 10;

        // Limit overCharge to a ratio above MaxEnergy
        float maxOverchargeRatio = 2;
        if (overchargeBySecond + currentStat > maxStat * maxOverchargeRatio)
        {
            overchargeBySecond = maxStat * maxOverchargeRatio - currentStat;
        }

        // Use Ki as a resource for overCharge
        if (CurrentKi > overchargeBySecond)
        {
            CurrentKi -= overchargeBySecond;
        }
        else
        {
            overchargeBySecond = CurrentKi;
            CurrentKi = 0;
        }

        return overchargeBySecond;
        // CurrentEnergy += overchargeBySecond;
        // 
    }
}
