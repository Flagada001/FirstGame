using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        transform.rotation = Quaternion.LookRotation(hit.point - transform.position);
        hasLaunched = true;
    }
}
