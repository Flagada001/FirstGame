using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class CombatStats : MonoBehaviour
    {
        // Spare hp
        private float maxKi;
        public float MaxKi { get => maxKi; set => maxKi = value; }

        // Melee combat damage
        private float maxPhysical;
        public float MaxPhysical { get => maxPhysical; set => maxPhysical = value; }

        // Movement speed, maybe Dodge rating
        private float maxSpeed;
        public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }

        // Special move damage
        private float maxEnergy;
        public float MaxEnergy { get => maxEnergy; set => maxEnergy = value; }

        // Return Total value of all Max stat
        public float MaxTotal { get => MaxPhysical + maxSpeed + maxEnergy + maxKi; }


        // All Stats will get weaker as you are damaged
        private float currentKi;
        public float CurrentKi { get => currentKi; set => currentKi = value; }

        private float currentPhysical;
        public float CurrentPhysical { get => currentPhysical; set => currentPhysical = value; }

        private float currentEnergy;
        public float CurrentEnergy { get => currentEnergy; set => currentEnergy = value; }

        private float currentSpeed;
        public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }

        public float CurrentTotal { get => currentPhysical + currentSpeed + currentEnergy + currentKi; }

        private bool isDead = false;
        public bool IsDead { get => isDead; set => isDead = value; }

        public virtual void ApplyDamage(float damage)
        {
            // Damage would kill the player
            if (CurrentTotal <= damage)
            {
                IsDead = true;
                Debug.Log(gameObject.name + " DIED");
                currentKi = 0;
                currentPhysical = 0;
                currentSpeed = 0;
                currentEnergy = 0;
                return;
            }

            // CurrentKi Take damage first, verify if overflow is needed
            if (currentKi > damage)
            {
                currentKi -= damage;
                return;
            }
            damage -= currentKi;
            currentKi = 0;

            // Split damage between all stats by their ratio
            float initialCurrentTotalValue = CurrentTotal - currentKi;
            currentPhysical -= (damage * currentPhysical / initialCurrentTotalValue);
            currentSpeed -= (damage * currentSpeed / initialCurrentTotalValue);
            currentEnergy -= (damage * currentEnergy / initialCurrentTotalValue);
        }

        public virtual void ApplyHealing(float healing)
        {
            float remainingHealing = 0;

            // Nothing happens if full hp
            if (CurrentTotal >= MaxTotal) { return; }

            // Calculate how much of each stat is missing, ignore if over max
            float missingPhysical = maxPhysical - currentPhysical;
            float missingEnergy = maxEnergy - currentEnergy;
            float missingSpeed = maxSpeed - currentSpeed;
            if (missingPhysical < 0) { missingPhysical = 0; }
            if (missingEnergy < 0) { missingEnergy = 0; }
            if (missingSpeed < 0) { missingSpeed = 0; }

            // Allow overflow healing from primary stats to currentKi
            float missingTotalExcludingKi = missingPhysical + missingEnergy + missingSpeed;
            if (missingTotalExcludingKi < healing)
            {
                remainingHealing = healing - (missingTotalExcludingKi);
                healing = (missingTotalExcludingKi);
            }

            // Apply healing to ratio of missing primary stat 
            if (missingTotalExcludingKi > 0)
            {
                currentPhysical += healing * missingPhysical / missingTotalExcludingKi;
                currentSpeed += healing * missingSpeed / missingTotalExcludingKi;
                currentEnergy += healing * missingEnergy / missingTotalExcludingKi;

            }
            if (remainingHealing == 0) { return; }

            // Calculate healing to Ki, make sure Ki does not bring CurrentTotal over MaxTotal
            float missingKi = MaxTotal - (currentPhysical + currentSpeed + currentEnergy);
            if (missingKi > remainingHealing)
            {
                currentKi += remainingHealing;
            }
            else
            {
                currentKi = missingKi;
            }
        }
    }
}