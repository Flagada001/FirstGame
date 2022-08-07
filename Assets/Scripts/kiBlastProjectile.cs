using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Characters;

public class KiBlastProjectile : MonoBehaviour
{
    // TODO : Better offset value for the projectile spawn
    // TODO : Better way to calculate speed and size modifier

    // only characterstat needed is Energy
    // Damage = energy
    // size = energy/2+3 min 0.5, max 2
    // speed = energy*15+2, min 10, max 30

    private float energy;
    public float Energy
    {
        get => energy;
        set
        {
            if (value <= 0)
            {
                Debug.Log(gameObject.name + " is destroyed");
                hasColided = true;
                Destroy(gameObject);
            }
            energy = value;
        }
    }
    // initialized and when launched

    public float Damage { get => Energy; }
    private float Speed { get => Mathf.Max(Mathf.Min(Energy * 15 + 2, 60), 10); }
    private float Size { get => Mathf.Max(Mathf.Min(Energy / 2, 1.5f), 0.5f); }
    private Vector3 alphaSize { get => new Vector3(Size / 2, Size / 2, Size); }

    private bool hasLaunched = false;
    private bool hasColided = false;

    private float despawnTimer;
    static float maxDespawnTimer = 0.01f;

    private CombatStats characterStats;
    public CombatStats CharacterStats { get => characterStats; }

    private Vector3 projectileOffset = new Vector3(0.2f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Initialize(CombatStats tempStat)
    {
        characterStats = tempStat;
        Energy = characterStats.CurrentEnergy;
        transform.localScale = alphaSize;
    }

    // Update is called once per frame
    void Update()
    {
        // Once colided the object take a moment to despawn
        if (hasColided)
        {
            despawnTimer += Time.deltaTime;
            if (despawnTimer >= maxDespawnTimer)
            {
                Destroy(gameObject);
            }
            return;
        }


        if (hasLaunched)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * Speed);
            return;
        }

        Energy = characterStats.CurrentEnergy;
        transform.localScale = alphaSize;
        transform.rotation = characterStats.gameObject.transform.rotation;
        transform.position = characterStats.gameObject.transform.position;
        transform.Translate(projectileOffset);
    }

    //
    void OnCollisionEnter(Collision col)
    {
        if (hasColided) { return; }
        if (col.gameObject.name == characterStats.gameObject.name) { return; }

        // Reduce damage and destroy projectile along the way
        if (col.gameObject.tag == "KiProjectile")
        {
            // Debug.Log("IMPACT");
            float damageBeforeImpact = Damage;
            Energy -= col.gameObject.GetComponent<KiBlastProjectile>().Damage;
            col.gameObject.GetComponent<KiBlastProjectile>().Energy -= damageBeforeImpact;
        }
        else
        {
            hasColided = true;
            // Debug.Log(col.gameObject.name + " received " + Damage);
            col.gameObject.SendMessage("ApplyDamage", Damage, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void launchProjectile(Vector3 target)
    {
        transform.rotation = Quaternion.LookRotation(target - transform.position);
        hasLaunched = true;
    }
}
