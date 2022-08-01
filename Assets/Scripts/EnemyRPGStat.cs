using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRPGStat : MonoBehaviour
{
    public float healthMax;
    public float healthCurrent;
    // public Texture2D healthBackground;
    // Start is called before the first frame update
    void Start()
    {
        healthMax = 2;
        healthCurrent = healthMax;
        Debug.Log("Max HP : " + healthCurrent);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        healthCurrent -= damage;
        Debug.Log("HP : " + healthCurrent);
        if (healthCurrent <= 0)
        {
            Destroy(gameObject);
        }
    }
}
