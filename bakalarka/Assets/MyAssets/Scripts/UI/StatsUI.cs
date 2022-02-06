using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    ExperienceManager xpManager;
    PlayerStats playerStats;
    Inventory inventory;

    public GameObject statsUI;

    public Image xpSlider;
    public Text levelText;
    public Text xpText;

    public Text strengthText;
    public Text dexterityText;
    public Text vitalityText;

    public Text statPointsText;

    void Awake()
    {
        xpManager = ExperienceManager.instance;
        xpManager.OnXPChanged += OnXPChanged;
    }

    void Start()
    {
        playerStats = PlayerStats.instance;
        playerStats.OnStatChanged += UpdateUI;

        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        strengthText.text = playerStats.strength.GetValue().ToString();
        dexterityText.text = playerStats.dexterity.GetValue().ToString();
        vitalityText.text = playerStats.vitality.GetValue().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Stats"))
        {
            statsUI.SetActive(!statsUI.activeSelf);
        }
    }

    void OnXPChanged(int maxXP, int currentXP, int level, int statPoints)
    {
        if (xpSlider != null)
        {
            float xpPercent = currentXP / (float)maxXP;
            xpSlider.fillAmount = xpPercent;
        }

        if (xpText != null)
        {
            xpText.text = currentXP + "/" + maxXP;
        }

        if (levelText != null)
        {
            levelText.text = "Level " + level;
        }

        if (statPointsText != null)
        {
            statPointsText.text = statPoints.ToString();
        }
    }

    void UpdateUI()
    {
        strengthText.text = playerStats.strength.GetValue().ToString();
        dexterityText.text = playerStats.dexterity.GetValue().ToString();
        vitalityText.text = playerStats.vitality.GetValue().ToString();
    }
}
