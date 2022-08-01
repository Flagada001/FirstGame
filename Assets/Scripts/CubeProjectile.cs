using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeProjectile : MonoBehaviour
{
    public float damage;
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
        new WaitForSeconds(10);
        if (col.gameObject.name != "Character")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            EnemyRPGStat other = (EnemyRPGStat)col.gameObject.GetComponent(typeof(EnemyRPGStat));
            other.TakeDamage(1);
        }

    }
}
