using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControls : MonoBehaviour
{
    private float XMoveSpeed = 4;
    private float XMoveInput;
    private float ZMoveSpeed = 4;
    private float ZMoveInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.mousePosition);
        transform.LookAt(Input.mousePosition);

        // Move the Character on a flat plane
        ZMoveInput = Input.GetAxis("Horizontal");
        XMoveInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * XMoveSpeed * XMoveInput);
        transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * ZMoveSpeed * ZMoveInput);

    }
}
