using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    private CharacterRPGStats characterStat;
    private GameObject kiBlast;
    public GameObject KiBlastPrefab;
    // Start is called before the first frame update
    void Start()
    {
        characterStat = gameObject.GetComponent<CharacterRPGStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void KiBlastPressed()
    {
        kiBlast = Instantiate(KiBlastPrefab, transform.position, transform.rotation);
        kiBlast.gameObject.GetComponent<kiBlastProjectile>().Initialize(characterStat);
    }

    public void KiBlastReleased()
    {
        kiBlast.gameObject.GetComponent<kiBlastProjectile>().launchProjectile();
    }
}
