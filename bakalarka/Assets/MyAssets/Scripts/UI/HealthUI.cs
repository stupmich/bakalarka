using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;

    float visibleTime = 5;
    float lastMadeVisibleTime;

    Transform ui;
    Image healthSlider;
    Transform cam;

    public bool boss;

    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main.transform;
        Vector3 scaleChange = new Vector3(1, 1, 1);

        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {

                ui = Instantiate(uiPrefab, c.transform ).transform;
                if (boss)
                {
                    ui.transform.localScale += scaleChange;
                }
               
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
        }

        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void OnHealthChanged(float healthPerc)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            lastMadeVisibleTime = Time.time;

            healthSlider.fillAmount = healthPerc;

            if (healthPerc <= 0)
            {
                Destroy(ui.gameObject);
            }
        }

    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;
            if (Time.time - lastMadeVisibleTime > visibleTime)
            {
                ui.gameObject.SetActive(false);
            }
        }

    }
}
