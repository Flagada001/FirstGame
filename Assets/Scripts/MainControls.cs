using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControls : MonoBehaviour
{
    //default value for movement and rotation speed
    public float xMoveSpeed = 10;
    public float zMoveSpeed = 10;
    public float rotateSpeed = 10;

    public GameObject kiBlastPreFab;

    //Variable that will inclue the keyboard value
    private float xMoveInput;
    private float zMoveInput;

    public GameObject statTab;

    private CharacterRPGStats characterStat;
    private GameObject kiBlast;

    void Start()
    {
        characterStat = (CharacterRPGStats)gameObject.GetComponent(typeof(CharacterRPGStats));
    }
    // Update is called once per frame
    void Update()
    {
        // Trace a Ray from the mouse position to the first object hit
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit); // find the object hit by the ray
        Vector3 targetDirection = hit.point - transform.position;
        float singleStep = rotateSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection - new Vector3(0, newDirection.y, 0));

        // Move the Character on a flat plane
        xMoveInput = Input.GetAxis("Horizontal");
        zMoveInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * xMoveSpeed * xMoveInput);
        transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * zMoveSpeed * zMoveInput);

        //Right click would trigger a kiBlast
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            kiBlast = Instantiate(kiBlastPreFab, transform.position, transform.rotation);
            kiBlast.gameObject.GetComponent<kiBlastProjectile>().Initialize(characterStat);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            kiBlast.gameObject.GetComponent<kiBlastProjectile>().launchProjectile();
        }


        //Open the stats interface
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (statTab.activeSelf)
            {
                statTab.SetActive(false);
            }
            else
            {
                statTab.SetActive(true);
            }
        }


    }
}
