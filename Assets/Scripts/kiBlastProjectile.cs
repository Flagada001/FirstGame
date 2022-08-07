using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Characters;

public class KiBlastProjectile : MonoBehaviour
{
    // TODO : Better offset value for the projectile spawn
    // TODO : Batter way to calculate speed and size modifier

    private float damage;
    public float Damage { get => damage; set => damage = value; }
    private float tempDamage;

    private float speed;
    private float size;
    private Vector3 alphaSize { get => new Vector3(size, size, size); }

    private bool hasLaunched = false;
    private bool hasColided = false;
    private float despawnTimer;
    static float maxDespawnTimer = 0.01f;

    private CombatStats characterStats;
    public CombatStats CharacterStats { get => characterStats; }




    // Start is called before the first frame update
    void Start()
    {
    }

    public void Initialize(CombatStats tempStat)
    {
        characterStats = tempStat;
        size = characterStats.CurrentEnergy / 2;
        damage = characterStats.CurrentEnergy;
        speed = characterStats.CurrentEnergy * 20;
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
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            return;
        }
        transform.localScale = alphaSize;

        transform.rotation = characterStats.gameObject.transform.rotation;
        transform.position = characterStats.gameObject.transform.position;
        transform.Translate(new Vector3(0.2f, 0, 0.7f));
    }

    //
    void OnCollisionEnter(Collision col)
    {
        if (hasColided) { return; }
        if (col.gameObject.name == characterStats.gameObject.name) { return; }
        if (col.gameObject.tag == "KiProjectile")
        {
            Debug.Log(characterStats.gameObject.name + " projectile damage " + damage);
            tempDamage -= col.gameObject.GetComponent<KiBlastProjectile>().Damage;
            size = tempDamage / 2;
            transform.localScale = alphaSize;
            Debug.Log(characterStats.gameObject.name + " projectile diminished damage " + tempDamage);
            if (tempDamage <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            hasColided = true;
            Debug.Log(col.gameObject.name + " received " + tempDamage);
            col.gameObject.SendMessage("ApplyDamage", tempDamage, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void launchProjectile(Vector3 target)
    {
        transform.rotation = Quaternion.LookRotation(target - transform.position);
        hasLaunched = true;
        damage = characterStats.CurrentEnergy;
        speed = characterStats.CurrentEnergy * 20;
        size = characterStats.CurrentEnergy / 2;
        tempDamage = damage;
    }
}
