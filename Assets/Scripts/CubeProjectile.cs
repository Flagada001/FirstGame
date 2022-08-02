using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeProjectile : MonoBehaviour
{
    private float damage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 20);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "Character")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            EnemyRPGStat other = (EnemyRPGStat)col.gameObject.GetComponent(typeof(EnemyRPGStat));
            other.TakeDamage(damage);
        }

    }

    public void setDamage(float val)
    {
        damage = val;
    }
}
