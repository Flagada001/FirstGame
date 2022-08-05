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

    // a Fix
    private RectTransform childBackfillBar;
    // The health bar that fill for each stats
    private RectTransform childPhysicalBar;
    private RectTransform childSpeedBar;
    private RectTransform childEnergyBar;
    private RectTransform childKiBar;

    // Start is called before the first frame update
    void Start()
    {
        // Create a health bar show on the UI canvas
        healthBar = Instantiate(HealthBarPrefab, transform.position, transform.rotation);
        mainCamera = GameObject.Find("Main Camera");
        ui = GameObject.Find("UI");
        healthBar.transform.SetParent(ui.transform.transform);

        // Child0 will always be the Backfill(MaxTotal), Last child must be CurrentKi
        // Physical,Speed,Energy order doesnt matter
        childBackfillBar = healthBar.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        childPhysicalBar = healthBar.transform.GetChild(1).gameObject.GetComponent<RectTransform>();
        childSpeedBar = healthBar.transform.GetChild(2).gameObject.GetComponent<RectTransform>();
        childEnergyBar = healthBar.transform.GetChild(3).gameObject.GetComponent<RectTransform>();
        childKiBar = healthBar.transform.GetChild(4).gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // If the Enemy is dead stop moving/showing the HealthBar
        if (gameObject.GetComponent<EnemyStats>() != null)
        {
            if (gameObject.GetComponent<EnemyStats>().IsDead)
            {
                Destroy(healthBar);
                return;
            }
        }

        // Keep the HealthBar above character
        characterPositionOnCanvas = mainCamera.GetComponent<Camera>().WorldToViewportPoint(transform.position);
        healthBar.transform.position = new Vector3(ui.GetComponent<RectTransform>().rect.width * characterPositionOnCanvas.x, ui.GetComponent<RectTransform>().rect.height * characterPositionOnCanvas.y, 0);
        // Update all 4 health bar
        CombatStats stats = gameObject.GetComponent<CombatStats>();


        float pos = -childBackfillBar.rect.width / 2;
        childPhysicalBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stats.CurrentPhysical / stats.MaxTotal * (childBackfillBar.rect.width));
        childPhysicalBar.anchoredPosition = new Vector3(pos + childPhysicalBar.rect.width / 2, 0, 0);
        pos += childPhysicalBar.rect.width;
        childSpeedBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stats.CurrentSpeed / stats.MaxTotal * (childBackfillBar.rect.width));
        childSpeedBar.anchoredPosition = new Vector3(pos + childSpeedBar.rect.width / 2, 0, 0);
        pos += childSpeedBar.rect.width;
        childEnergyBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stats.CurrentEnergy / stats.MaxTotal * (childBackfillBar.rect.width));
        childEnergyBar.anchoredPosition = new Vector3(pos + childEnergyBar.rect.width / 2, 0, 0);
        pos += childEnergyBar.rect.width;
        childKiBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stats.CurrentKi / stats.MaxTotal * (childBackfillBar.rect.width));
        childKiBar.anchoredPosition = new Vector3(pos + childKiBar.rect.width / 2, 0, 0);
        pos += childKiBar.rect.width;
    }
}
