using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTotal : MonoBehaviour
{
    public GameObject character;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // character.CharacterRPGStats.
        CharacterRPGStats other = (CharacterRPGStats)character.gameObject.GetComponent(typeof(CharacterRPGStats));

        transform.GetComponent<TMPro.TextMeshProUGUI>().text = string.Format("{0:f1}/{1:f1}", other.ReturnCurrentTotal(), other.ReturnMaxTotal());
    }
}
