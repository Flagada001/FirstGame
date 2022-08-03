using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRPGStat : MonoBehaviour
{
    public float healthMax;
    public float healthCurrent;
    private bool isDead = false;
    private float deadTimer;
    static float maxDeadTimer = 2;
    // public Texture2D healthBackground;
    // Start is called before the first frame update
    void Start()
    {
        healthMax = 2;
        healthCurrent = healthMax;
        Debug.Log(string.Format("Max HP : {0:f1}", healthCurrent));
    }

    // Update is called once per frame
    void Update()
    {
        //Allow game object to fall dead after death for maxDeadTimer second
        if (isDead)
        {
            deadTimer += Time.deltaTime; //Will Count second
            if (deadTimer >= maxDeadTimer)
            {
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        healthCurrent -= damage;
        Debug.Log(string.Format("HP : {0:f1}", healthCurrent));
        if (healthCurrent <= 0)
        {
            isDead = true;
            gameObject.GetComponent<NavMeshAgent>().enabled = false; //Disable the movement AI from the game object
        }
    }
}
