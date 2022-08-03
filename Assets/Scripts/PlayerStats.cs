using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Spare hp
    private float maxKi;
    public float MaxKi { get => maxKi; }

    // Melee combat damage
    private float maxPhysical;
    public float MaxPhysical { get => maxPhysical; }

    // Movement speed, maybe Dodge rating
    private float maxSpeed;
    public float MaxSpeed { get => maxSpeed; }

    // Special move damage
    private float maxEnergy;
    public float MaxEnergy { get => maxEnergy; }

    // Return Total value of all Max stat
    public float MaxTotal { get => maxPhysical + maxSpeed + maxEnergy + maxKi; }


    // All Stats will get weaker as you are damaged
    private float currentKi;
    public float CurrentKi { get => currentKi; }

    private float currentPhysical;
    public float CurrentPhysical { get => currentPhysical; }

    private float currentEnergy;
    public float CurrentEnergy { get => currentEnergy; }

    private float currentSpeed;
    public float CurrentSpeed { get => currentSpeed; }

    public float CurrentTotal { get => CurrentPhysical + CurrentSpeed + CurrentEnergy + CurrentKi; }


    // Start is called before the first frame update
    void Start()
    {
        maxKi = 1;
        maxPhysical = 1;
        maxSpeed = 2;
        maxEnergy = 1;
        currentKi = maxKi;
        currentPhysical = maxPhysical;
        currentSpeed = maxSpeed;
        currentEnergy = maxEnergy;
    }


    public void TakeDamage(float damage)
    {
        // CurrentKi Take damage first as SpareHP
        damage -= currentKi;
        if (damage < 0)
        {
            currentKi = -damage;
            return;
        }
        currentKi = 0;

        // Damage would kill the player
        if (CurrentTotal <= damage)
        {
            // TODO: fail state
            currentPhysical = 0;
            currentSpeed = 0;
            currentEnergy = 0;
            return;
        }

        // Split damage between all stats by their ratio
        float currentTotalValue = CurrentTotal;
        currentPhysical -= (damage * currentPhysical / currentTotalValue);
        currentSpeed -= (damage * currentSpeed / currentTotalValue);
        currentEnergy -= (damage * currentEnergy / currentTotalValue);

    }
}
