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

        childBackfillBar = healthBar.transform.Find("Backfill").GetComponent<RectTransform>();
        childPhysicalBar = childBackfillBar.Find("Physical").GetComponent<RectTransform>();
        childSpeedBar = childBackfillBar.Find("Speed").GetComponent<RectTransform>();
        childEnergyBar = childBackfillBar.Find("Energy").GetComponent<RectTransform>();
        childKiBar = childBackfillBar.Find("Ki").GetComponent<RectTransform>();
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
        float totalScaledToWidth = childBackfillBar.rect.width / stats.MaxTotal;

        childPhysicalBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stats.CurrentPhysical * totalScaledToWidth);
        childPhysicalBar.anchoredPosition = new Vector3(positionInBackfillBar, 0, 0);
        positionInBackfillBar += childPhysicalBar.rect.width;
        childSpeedBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stats.CurrentSpeed * totalScaledToWidth);
        childSpeedBar.anchoredPosition = new Vector3(positionInBackfillBar, 0, 0);
        positionInBackfillBar += childSpeedBar.rect.width;
        childEnergyBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stats.CurrentEnergy * totalScaledToWidth);
        childEnergyBar.anchoredPosition = new Vector3(positionInBackfillBar, 0, 0);
        positionInBackfillBar += childEnergyBar.rect.width;
        childKiBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stats.CurrentKi * totalScaledToWidth);
        childKiBar.anchoredPosition = new Vector3(positionInBackfillBar, 0, 0);
    }
}
