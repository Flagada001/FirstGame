using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControls : MonoBehaviour
{
    public float xMoveSpeed = 4;
    private float xMoveInput;
    public float zMoveSpeed = 4;
    private float zMoveInput;
    public float rotateSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Input.GetAxis("Mouse X"));
        // transform.Rotate(new Vector3(0, 1, 0) * Input.GetAxis("Mouse X"));

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 targetDirection = hit.point - transform.position;
        float singleStep = rotateSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        newDirection -= new Vector3(0, newDirection.y, 0);
        transform.rotation = Quaternion.LookRotation(newDirection);

        // Move the Character on a flat plane
        xMoveInput = Input.GetAxis("Horizontal");
        zMoveInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * xMoveSpeed * xMoveInput);
        transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * zMoveSpeed * zMoveInput);

    }
}
