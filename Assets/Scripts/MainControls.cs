using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControls : MonoBehaviour
{
    //default value for movement and rotation speed
    public float xMoveSpeed = 10;
    public float zMoveSpeed = 10;
    public float rotateSpeed = 10;

    //Variable that will inclue the keyboard value
    private float xMoveInput;
    private float zMoveInput;

    // Update is called once per frame
    void Update()
    {
        // Trace a Ray from the mouse position to the first object hit
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // find the object hit by the ray and asigned it to hit
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 targetDirection = hit.point - transform.position;
        //Change rotation speed from ByFrame to PerSecond
        float singleStep = rotateSpeed * Time.deltaTime;
        //
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        //remove any Y rotation
        newDirection -= new Vector3(0, newDirection.y, 0);
        transform.rotation = Quaternion.LookRotation(newDirection);

        // Move the Character on a flat plane
        xMoveInput = Input.GetAxis("Horizontal");
        zMoveInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * xMoveSpeed * xMoveInput);
        transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * zMoveSpeed * zMoveInput);

    }
}
