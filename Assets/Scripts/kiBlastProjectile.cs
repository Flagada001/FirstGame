using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class kiBlastProjectile : MonoBehaviour
{
    private float characterEnergyValue;

    private float damage;
    private float speed;
    private float size;
    private float impactForce;

    private bool hasLaunched = false;
    private bool hasColided = false;
    private float despawnTimer;
    static float maxDespawnTimer = 0.01f;
    private float chargeTimer;

    private RaycastHit targetAimedAt;
    private CharacterRPGStats characterStat;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Initialize(CharacterRPGStats tempStat)
    {
        characterStat = tempStat;
        characterEnergyValue = characterStat.ReturnCurrentEnergy();
        impactForce = speed * size * 100;
        speed = characterEnergyValue * 20;
        size = characterEnergyValue / 2f;
        damage = characterEnergyValue;
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
            transform.rotation = characterStat.gameObject.transform.rotation;
            transform.position = characterStat.gameObject.transform.position;
            transform.Translate(new Vector3(0.2f, 0, 0.7f));
            chargeTimer += Time.deltaTime;


            if (characterEnergyValue * (chargeTimer + 1) / 2f > 1)
            {
                size = 1;
            }
            else
            {
                size = characterEnergyValue * (chargeTimer + 1) / 2f;
            }

            speed = characterEnergyValue * (chargeTimer + 1) * 20;
            damage = characterEnergyValue * (chargeTimer + 1);
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
        if (col.gameObject.name != "Character")
        {
            hasColided = true;
        }
        if (col.gameObject.tag == "Enemy")
        {
            EnemyRPGStat other = (EnemyRPGStat)col.gameObject.GetComponent(typeof(EnemyRPGStat));
            other.TakeDamage(damage);
        }

    }

    private void setCharacterEnergyValue(float val)
    {
        characterEnergyValue = val;
    }

    public void launchProjectile()
    {
        float yAimOffset = 0;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        // myNavHit.position.y get the floor Height the character is standing on
        NavMeshHit myNavHit;
        if (NavMesh.SamplePosition(transform.position, out myNavHit, 10, -1))
        {
            if (hit.transform.gameObject.tag == "Enemy")
            {
                yAimOffset = 0;
            }
            else
            {
                yAimOffset = transform.position.y - myNavHit.position.y;
            }
        }

        transform.rotation = Quaternion.LookRotation(hit.point - transform.position + new Vector3(0, yAimOffset, 0));
        hasLaunched = true;
    }
}
