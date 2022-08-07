using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    //dependent on character Stats Speed
    public float MoveSpeed { get { return characterStat.CurrentSpeed * 15 + 1; } }
    public float RotateSpeed { get { return characterStat.CurrentSpeed * 2 + 5; } }

    private PlayerStats characterStat;

    // Start is called before the first frame update
    void Start()
    {
        characterStat = gameObject.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) { return; }
        //Get object mouse point at
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        Vector3 targetDirection = hit.point - transform.position;
        float singleStep = RotateSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        // Substract Y to the rotation to prevent looking at the ground
        transform.rotation = Quaternion.LookRotation(newDirection - new Vector3(0, newDirection.y, 0));

        float xMoveInput = Input.GetAxis("Horizontal");
        float zMoveInput = Input.GetAxis("Vertical");
        transform.position += new Vector3(1, 0, 0) * Time.deltaTime * MoveSpeed * xMoveInput;
        transform.position += new Vector3(0, 0, 1) * Time.deltaTime * MoveSpeed * zMoveInput;

        // in case of camera rotation based movement
        // transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * xMoveSpeed * xMoveInput);
        // transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * zMoveSpeed * zMoveInput);
    }
}
