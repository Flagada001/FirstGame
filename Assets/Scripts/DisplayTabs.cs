using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTabs : MonoBehaviour
{
    public GameObject character;
    public GameObject totalToDisplay;
    public GameObject kiToDisplay;
    public GameObject physicalToDisplay;
    public GameObject speedToDisplay;
    public GameObject energyToDisplay;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // character.CharacterRPGStats.
        PlayerStats statToDisplay = (PlayerStats)character.gameObject.GetComponent(typeof(PlayerStats));
        totalToDisplay.transform.GetComponent<TMPro.TextMeshProUGUI>().text = string.Format("{0:f1}/{1:f1}", statToDisplay.CurrentTotal, statToDisplay.MaxTotal);
        kiToDisplay.transform.GetComponent<TMPro.TextMeshProUGUI>().text = string.Format("{0:f1}/{1:f1}", statToDisplay.CurrentKi, statToDisplay.MaxKi);
        physicalToDisplay.transform.GetComponent<TMPro.TextMeshProUGUI>().text = string.Format("{0:f1}/{1:f1}", statToDisplay.CurrentPhysical, statToDisplay.MaxPhysical);
        speedToDisplay.transform.GetComponent<TMPro.TextMeshProUGUI>().text = string.Format("{0:f1}/{1:f1}", statToDisplay.CurrentSpeed, statToDisplay.MaxSpeed);
        energyToDisplay.transform.GetComponent<TMPro.TextMeshProUGUI>().text = string.Format("{0:f1}/{1:f1}", statToDisplay.CurrentEnergy, statToDisplay.MaxEnergy);
    }
}
