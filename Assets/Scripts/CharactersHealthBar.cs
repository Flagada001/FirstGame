using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactersHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCam;

    void Start()
    {
        Vector3 healthBarPosition = mainCam.GetComponent<Camera>().WorldToScreenPoint(transform.position);
        Debug.Log(healthBarPosition);
        GameObject ImgHealthBar = new GameObject("HealthBar");
        ImgHealthBar.AddComponent<RectTransform>().transform.position = transform.position;
        ImgHealthBar.AddComponent<Image>();
        //Create empty oject above head
        //add back fill
        //add 4 colored line for Physical/Speed/Energy/Ki
    }

    // Update is called once per frame
    void Update()
    {
        //keep the object above character
        //update all 4 health bar
    }
}
