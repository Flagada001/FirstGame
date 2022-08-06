using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

public class CharactersHealthBar : MonoBehaviour
{
    public GameObject HealthBarPrefab;

    private GameObject mainCamera; // Used to find the position on screen
    private GameObject ui; // Used to know how large the screen is in pixel
    private Vector2 characterPositionOnCanvas;
    private GameObject healthBar;

    // Used for the full width of the health bar
    private RectTransform childBackfillBar;
    // The health bar that fill for each stats
    private RectTransform childPhysicalBar;
    private RectTransform childSpeedBar;
    private RectTransform childEnergyBar;
    private RectTransform childKiBar;

    private CombatStats stats;
    private static Vector3 offsetAboveHead = new Vector3(0, 1f, 0);

    // Start is called before the first frame update
    void Start()
    {
        // Create a health bar show on the UI canvas
        healthBar = Instantiate(HealthBarPrefab, transform.position, transform.rotation);
        mainCamera = GameObject.Find("Main Camera");
        ui = GameObject.Find("UI");
        healthBar.transform.SetParent(ui.transform.transform);
        stats = gameObject.GetComponent<CombatStats>();

        // Child0 will always be the Backfill(MaxTotal), Last child must be CurrentKi
        // Physical,Speed,Energy order doesnt matter
        childBackfillBar = healthBar.transform.GetChild(0).GetComponent<RectTransform>();
        childPhysicalBar = healthBar.transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>();
        childSpeedBar = healthBar.transform.GetChild(0).transform.GetChild(1).GetComponent<RectTransform>();
        childEnergyBar = healthBar.transform.GetChild(0).transform.GetChild(2).GetComponent<RectTransform>();
        childKiBar = healthBar.transform.GetChild(0).transform.GetChild(3).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (stats.IsDead)
        {
            Destroy(healthBar);
            return;
        }

        // Keep the HealthBar above character
        healthBar.transform.position = mainCamera.GetComponent<Camera>().WorldToScreenPoint(transform.position + offsetAboveHead);

        // Update all 4 health bar width
        float positionInBackfillBar = 0;
        float totalAndWidth = stats.MaxTotal * childBackfillBar.rect.width;

        childPhysicalBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stats.CurrentPhysical / totalAndWidth);
        childPhysicalBar.anchoredPosition = new Vector3(positionInBackfillBar, 0, 0);
        positionInBackfillBar += childPhysicalBar.rect.width;
        childSpeedBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stats.CurrentSpeed / totalAndWidth);
        childSpeedBar.anchoredPosition = new Vector3(positionInBackfillBar, 0, 0);
        positionInBackfillBar += childSpeedBar.rect.width;
        childEnergyBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stats.CurrentEnergy / totalAndWidth);
        childEnergyBar.anchoredPosition = new Vector3(positionInBackfillBar, 0, 0);
        positionInBackfillBar += childEnergyBar.rect.width;
        childKiBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stats.CurrentKi / totalAndWidth);
        childKiBar.anchoredPosition = new Vector3(positionInBackfillBar, 0, 0);
    }
}
