using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Characters;

public class EnemyRPGStat : CombatStats
{
    private bool isDead = false;
    public bool IsDead { get => isDead; set => isDead = value; }

    private float deadTimer;
    static float maxDeadTimer = 2;

    // public Texture2D healthBackground;
    // Start is called before the first frame update
    void Start()
    {
        MaxKi = 0;
        MaxPhysical = 1;
        MaxSpeed = 1;
        MaxEnergy = 0;
        CurrentKi = MaxKi;
        CurrentPhysical = MaxPhysical;
        CurrentSpeed = MaxSpeed;
        CurrentEnergy = MaxEnergy;
    }


    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<NavMeshAgent>().speed = CurrentSpeed * 2 + 1;

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


    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (CurrentTotal <= 0)
        {
            IsDead = true;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }
    }
}
