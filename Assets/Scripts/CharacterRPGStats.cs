using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Placehodler for any RPG element and statistic of the main Character

public class CharacterRPGStats : MonoBehaviour
{

    public float maxKi;
    public float currentKi;
    public float maxPhysical;
    public float currentPhysical;
    public float maxSpeed;
    public float currentSpeed;
    public float maxEnergy;
    public float currentEnergy;

    // Start is called before the first frame update
    void Start()
    {
        maxKi = 1;
        maxPhysical = 1;
        maxSpeed = 1;
        maxEnergy = 1;
        currentKi = maxKi;
        currentPhysical = maxPhysical;
        currentSpeed = maxSpeed;
        currentEnergy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        if (damage <= currentKi)
        {
            currentKi -= damage;
        }
        else
        {
            damage -= currentKi;
            currentPhysical -= damage / 3;
            currentSpeed -= damage / 3;
            currentEnergy -= damage / 3;
        }
    }

    public float ReturnCurrentTotal()
    {
        return currentPhysical + currentSpeed + currentEnergy + currentKi;
    }
    public float ReturnMaxTotal()
    {
        return maxPhysical + maxSpeed + maxEnergy + maxKi;
    }

    //Spawn and throw a cude at the target
    public void PewPew()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // cube.AddComponent(typeof(Rigidbody));
        cube.AddComponent(typeof(CubeProjectile));
        cube.transform.rotation = transform.rotation;
        cube.transform.position = transform.position;
        CubeProjectile other = (CubeProjectile)cube.gameObject.GetComponent(typeof(CubeProjectile));
        other.setDamage(currentEnergy);
    }
}
