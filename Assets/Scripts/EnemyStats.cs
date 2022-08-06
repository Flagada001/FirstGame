using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Characters;

public class EnemyStats : CombatStats
{
    private float deadTimer;
    static float maxDeadTimer = 2;

    // public Texture2D healthBackground;
    // Start is called before the first frame update
    void Start()
    {
        MaxKi = 0;
        MaxPhysical = 0;
        MaxSpeed = 1;
        MaxEnergy = 1;
        CurrentKi = MaxKi;
        CurrentPhysical = MaxPhysical;
        CurrentSpeed = MaxSpeed;
        CurrentEnergy = MaxEnergy;
    }


    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<NavMeshAgent>().speed = CurrentSpeed * 4 + 1;

        //Allow game object to fall dead after death for maxDeadTimer second
        if (IsDead)
        {
            deadTimer += Time.deltaTime; //Will Count second
            if (deadTimer >= maxDeadTimer)
            {
                Destroy(gameObject);
            }
        }
    }


    public override void ApplyDamage(float damage)
    {
        base.ApplyDamage(damage);
        if (IsDead)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
