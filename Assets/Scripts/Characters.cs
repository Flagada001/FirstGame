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

        public float CurrentTotal { get => CurrentPhysical + CurrentSpeed + CurrentEnergy + CurrentKi; }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void ApplyDamage(float damage)
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
            float initialCurrentTotalValue = CurrentTotal;
            currentPhysical -= (damage * currentPhysical / initialCurrentTotalValue);
            currentSpeed -= (damage * currentSpeed / initialCurrentTotalValue);
            currentEnergy -= (damage * currentEnergy / initialCurrentTotalValue);
        }
        public virtual void ApplyHealing(float healing)
        {
            if (CurrentTotal >= MaxTotal) { return; }

            float missingPhysical = MaxPhysical - CurrentPhysical;
            float missingEnergy = MaxEnergy - CurrentEnergy;
            float missingSpeed = MaxSpeed - CurrentSpeed;
            float missingTotalExcludingKi = missingPhysical + missingEnergy + missingSpeed;

            float remainingHealing = healing - missingTotalExcludingKi;
            if (remainingHealing >= 0)
            {
                healing = missingTotalExcludingKi;
            }
            else
            {
                remainingHealing = 0;
            }

            if (missingTotalExcludingKi > 0)
            {
                CurrentPhysical += healing * missingPhysical / missingTotalExcludingKi;
                CurrentSpeed += healing * missingSpeed / missingTotalExcludingKi;
                CurrentEnergy += healing * missingEnergy / missingTotalExcludingKi;
            }

            if (CurrentKi + remainingHealing > MaxKi)
            {
                CurrentKi = MaxKi;
            }
            else
            {
                CurrentKi += remainingHealing;
            }
        }
    }
}