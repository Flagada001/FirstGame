using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

public class PlayerStats : CombatStats
{
    private float countSecond = 0;
    private float gainModifier = 10;

    // Start is called before the first frame update
    void Start()
    {
        MaxKi = 0;
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
        // if (MaxTotal - CurrentTotal > 0) { Debug.Log("Initial Healing" + (healing)); }
        float initalCurrentTotal = CurrentTotal;
        base.ApplyHealing(healing);
        GainMoreKi(CurrentTotal - initalCurrentTotal);
        // if (CurrentTotal - initalCurrentTotal > 0) { Debug.Log("Healed" + (CurrentTotal - initalCurrentTotal)); }
    }

    // Ki increase by healing at a rate of 1 new Ki for 100 ki Healed
    private void GainMoreKi(float gains)
    {

        MaxKi += gains / gainModifier;
        CurrentKi += gains / gainModifier;

    }

    // Energy Increased by overcharging
    public void GainMoreEnergy(float gains)
    {
        if (MaxKi < gains)
        {
            gains = MaxKi;
        }
        MaxEnergy += gains / gainModifier;
        CurrentEnergy += gains / gainModifier;
        MaxKi -= gains / gainModifier;
        if (CurrentKi > MaxKi) { CurrentKi = MaxKi; }
    }

    // Physical Increased by overcharging
    public void GainMorePhysical(float gains)
    {
        if (MaxKi < gains)
        {
            gains = MaxKi;
        }
        MaxPhysical += gains / gainModifier;
        CurrentPhysical += gains / gainModifier;
        MaxKi -= gains / gainModifier;
        if (CurrentKi > MaxKi) { CurrentKi = MaxKi; }
    }

    // Speed Increased by overcharging
    public void GainMoreSpeed(float gains)
    {
        if (MaxKi < gains)
        {
            gains = MaxKi;
        }
        MaxSpeed += gains / gainModifier;
        CurrentSpeed += gains / gainModifier;
        MaxKi -= gains / gainModifier;
        if (CurrentKi > MaxKi) { CurrentKi = MaxKi; }
    }

    public float OverchargeEnergy()
    {
        // Calculate how much Stat I want to transfer By second
        float overchargeBySecond = Time.deltaTime * CurrentTotal / CurrentEnergy / 10;

        // Limit overCharge to a ratio above MaxEnergy
        float maxOverchargeRatio = 2;
        if (overchargeBySecond + CurrentEnergy > MaxEnergy * maxOverchargeRatio)
        {
            overchargeBySecond = MaxEnergy * maxOverchargeRatio - CurrentEnergy;
        }

        float remainingOverchargeBySecond = overchargeBySecond;
        // Use Ki as a resource for overCharge
        if (CurrentKi > overchargeBySecond)
        {
            CurrentKi -= overchargeBySecond;
            CurrentEnergy += overchargeBySecond;
            return overchargeBySecond;
        }
        else
        {
            remainingOverchargeBySecond -= CurrentKi;
            CurrentEnergy += CurrentKi;
            CurrentKi = 0;
        }

        // apply remaining overchargeBySecond to a ratio
        float minCurrentPhysical = CurrentPhysical - 0.1f;
        if (minCurrentPhysical < 0) { minCurrentPhysical = 0; }
        // Debug.Log("Phys : " + minCurrentPhysical);
        float minCurrentSpeed = CurrentSpeed - 0.1f;
        if (minCurrentSpeed < 0) { minCurrentSpeed = 0; }
        float currentPhysicalSpeed = minCurrentPhysical + minCurrentSpeed;
        if (currentPhysicalSpeed == 0) { return overchargeBySecond - remainingOverchargeBySecond; };
        Debug.Log("Speed : " + minCurrentPhysical + "; Overchagre " + remainingOverchargeBySecond);


        CurrentPhysical -= remainingOverchargeBySecond * minCurrentPhysical / currentPhysicalSpeed;
        CurrentSpeed -= remainingOverchargeBySecond * minCurrentSpeed / currentPhysicalSpeed;
        CurrentEnergy += remainingOverchargeBySecond;

        return overchargeBySecond;
    }


    public float OverchargePhysical()
    {
        // Calculate how much Stat I want to transfer By second
        float overchargeBySecond = Time.deltaTime * CurrentTotal / CurrentPhysical / 10;

        // Limit overcharge to a ratio above MaxPhysical
        float maxOverchargeRatio = 2;
        if (overchargeBySecond + CurrentPhysical > MaxPhysical * maxOverchargeRatio)
        {
            overchargeBySecond = MaxPhysical * maxOverchargeRatio - CurrentPhysical;
        }

        float remainingOverchargeBySecond = overchargeBySecond;
        // Use Ki as a resource for overCharge
        if (CurrentKi > overchargeBySecond)
        {
            CurrentKi -= overchargeBySecond;
            CurrentPhysical += overchargeBySecond;
            return overchargeBySecond;
        }
        else
        {
            remainingOverchargeBySecond -= CurrentKi;
            CurrentPhysical += CurrentKi;
            CurrentKi = 0;
        }

        // apply remaining overchargeBySecond to a ratio
        float minCurrentEnergy = CurrentEnergy - 0.1f;
        if (minCurrentEnergy < 0) { minCurrentEnergy = 0; }
        float minCurrentSpeed = CurrentSpeed - 0.1f;
        if (minCurrentSpeed < 0) { minCurrentSpeed = 0; }
        float currentEnergySpeed = minCurrentSpeed + minCurrentEnergy;
        if (currentEnergySpeed == 0) { return overchargeBySecond - remainingOverchargeBySecond; };


        CurrentEnergy -= remainingOverchargeBySecond * minCurrentEnergy / currentEnergySpeed;
        CurrentSpeed -= remainingOverchargeBySecond * minCurrentSpeed / currentEnergySpeed;
        CurrentPhysical += remainingOverchargeBySecond;

        return overchargeBySecond;
    }

    public float OverchargeSpeed()
    {
        // Calculate how much Stat I want to transfer By second
        float overchargeBySecond = Time.deltaTime * CurrentTotal / CurrentSpeed / 10;

        // Limit overcharge to a ratio above MaxSpeed
        float maxOverchargeRatio = 2;
        if (overchargeBySecond + CurrentSpeed > MaxSpeed * maxOverchargeRatio)
        {
            overchargeBySecond = MaxSpeed * maxOverchargeRatio - CurrentSpeed;
        }

        float remainingOverchargeBySecond = overchargeBySecond;
        // Use Ki as a resource for overCharge
        if (CurrentKi > overchargeBySecond)
        {
            CurrentKi -= overchargeBySecond;
            CurrentSpeed += overchargeBySecond;
            return overchargeBySecond;
        }
        else
        {
            remainingOverchargeBySecond -= CurrentKi;
            CurrentPhysical += CurrentKi;
            CurrentKi = 0;
        }

        // apply remaining overchargeBySecond to a ratio
        float minCurrentEnergy = CurrentEnergy - 0.1f;
        if (minCurrentEnergy < 0) { minCurrentEnergy = 0; }
        float minCurrentPhysical = CurrentPhysical - 0.1f;
        if (minCurrentPhysical < 0) { minCurrentPhysical = 0; }
        float minCurrentPhysicalEnergy = minCurrentPhysical + minCurrentEnergy;
        if (minCurrentPhysicalEnergy == 0) { return overchargeBySecond - remainingOverchargeBySecond; };


        CurrentPhysical -= remainingOverchargeBySecond * minCurrentPhysical / minCurrentPhysicalEnergy;
        CurrentEnergy -= remainingOverchargeBySecond * minCurrentEnergy / minCurrentPhysicalEnergy;
        CurrentSpeed += remainingOverchargeBySecond;

        return overchargeBySecond;
    }

    // public void RebalanceStats()
    // {
    //     // TODO : Return all 3 stats to the same ratio over time
    //     // if any of 3 current is above
    //     // reduce it by second and apply healing?
    //     float overMaxPhysical = CurrentPhysical - MaxPhysical;
    //     float overMaxEnergy = CurrentEnergy - MaxEnergy;
    //     float overMaxSpeed = CurrentSpeed - MaxSpeed;
    //     if (overMaxPhysical < 0) { overMaxPhysical = 0; }
    //     if (overMaxEnergy < 0) { overMaxEnergy = 0; }
    //     if (overMaxSpeed < 0) { overMaxSpeed = 0; }


    // }
}
