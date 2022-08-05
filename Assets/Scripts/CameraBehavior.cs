using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject character;

    // Update is called once per frame
    void LateUpdate()
    {
        // Camera Follow the player
        transform.position = character.transform.position;

        //Can move the camera if Right mouse is pressed
        if (Input.GetAxis("Fire3") > 0)
        {
            //Move Camera Up and down
            transform.Rotate(new Vector3(1, 0, 0) * Input.GetAxis("Mouse Y"));

            //Move Camera left and Right
            transform.Rotate(new Vector3(0, 1, 0) * Input.GetAxis("Mouse X"));

            //Force camera rotation to be 0 on Z axis
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
        }
    }
}
