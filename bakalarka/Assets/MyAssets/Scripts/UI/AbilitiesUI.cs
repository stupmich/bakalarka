using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesUI : MonoBehaviour
{
    public GameObject abilitiesUI;

    ExperienceManager xpManager;

    public Text abilityPointsText;

    void Start()
    {
        xpManager = ExperienceManager.instance;
        xpManager.OnAbilityPointsChanged += OnAbilityPointsChanged;
    }

    void OnAbilityPointsChanged(int pAbilityPoints)
    {
        if (abilityPointsText != null)
        {
            abilityPointsText.text = pAbilityPoints.ToString();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Abilities"))
        {
            abilitiesUI.SetActive(!abilitiesUI.activeSelf);
        }
    }
}
