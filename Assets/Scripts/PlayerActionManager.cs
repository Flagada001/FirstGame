using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerActionManager : MonoBehaviour
{
    private PlayerStats characterStat;
    private GameObject kiBlast;
    public GameObject KiBlastPrefab;
    // Start is called before the first frame update
    void Start()
    {
        characterStat = gameObject.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpeedCharge()
    {
        // TODO : When pressing a key would raise speed value higher, more then its max?, reduce other stats in exchange?
        // TODO : Healing should ignore stats above their max
        // TODO : A method to balance stats ratio, transfering Ki First then other 2 stats in equal ratio
    }

    public void KiBlastPressed()
    {
        kiBlast = Instantiate(KiBlastPrefab, transform.position, transform.rotation);
        kiBlast.gameObject.GetComponent<KiBlastProjectile>().Initialize(characterStat);
    }

    public void KiBlastReleased()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        float yAimOffset = 0;
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
        kiBlast.gameObject.GetComponent<KiBlastProjectile>().launchProjectile(hit.point + new Vector3(0, yAimOffset, 0));
    }
}
