using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Characters;

public class kiBlastProjectile : MonoBehaviour
{
    private float power;

    private float damage;
    private float speed;
    private float size;
    private float impactForce;

    private bool hasLaunched = false;
    private bool hasColided = false;
    private float despawnTimer;
    static float maxDespawnTimer = 0.01f;
    private float chargeTimer;

    private CombatStats characterStats;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Initialize(CombatStats tempStat)
    {
        characterStats = tempStat;
        power = characterStats.CurrentEnergy;
        impactForce = speed * size * 100;
        speed = power * 20;
        size = power / 2f;
        damage = power;
        transform.localScale = new Vector3(size, size, size);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLaunched)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else
        {
            transform.rotation = characterStats.gameObject.transform.rotation;
            transform.position = characterStats.gameObject.transform.position;
            transform.Translate(new Vector3(0.2f, 0, 0.7f));
            chargeTimer += Time.deltaTime;


            if (power * (chargeTimer + 1) / 2f > 1)
            {
                size = 1;
            }
            else
            {
                size = power * (chargeTimer + 1) / 2f;
            }

            speed = power * (chargeTimer + 1) * 20;
            damage = power * (chargeTimer + 1);
            transform.localScale = new Vector3(size, size, size);
        }
        if (hasColided)
        {
            despawnTimer += Time.deltaTime;
            if (despawnTimer >= maxDespawnTimer)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == characterStats.gameObject.name) { return; }
        Debug.Log(col.gameObject.name + " " + characterStats.gameObject.name);
        hasColided = true;
        col.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);

    }

    private void setCharacterEnergyValue(float val)
    {
        power = val;
    }

    public void launchProjectile(Vector3 target)
    {
        transform.rotation = Quaternion.LookRotation(target - transform.position);
        hasLaunched = true;
    }
}
