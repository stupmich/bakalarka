using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarUI : MonoBehaviour
{
    [SerializeField]
    private PlayerAbilities abilities;
    [SerializeField]
    private PlayerStats stats;

    public Image ability1;
    public Image ability2;
    public Image ability3;
    public Image ability4;
    public Image ability5;

    public Image cooldown1;
    public Image cooldown2;
    public Image cooldown3;
    public Image cooldown4;
    public Image cooldown5;

    public Image health;
    public Image rage;
    private void Awake()
    {
        stats.OnRageChanged += UpdateRageUI;
        stats.OnHealthChanged += UpdateHealthUI;

        abilities.OnAbilityLearned += OnAbilityLearned;
    }
    // Start is called before the first frame update
    void Start()
    {
        abilities = PlayerAbilities.instance;
        abilities.OnAbilityLearned += OnAbilityLearned;
    }

    public void OnAbilityLearned(Ability ability)
    {
        switch (abilities.abilities.Count)
        {
            case 1:
                ability5.sprite = ability.abilityImage;
                break;
            case 2:
                ability1.sprite = ability.abilityImage;
                break;
            case 3:
                ability2.sprite = ability.abilityImage;
                break;
            case 4:
                ability3.sprite = ability.abilityImage;
                break;
            case 5:
                ability4.sprite = ability.abilityImage;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        int counter = 1;
        foreach (PlayerAbility ab in abilities.abilities)
        {
            float cooldownPerct = ab.GetCurrentCooldownTime() / (float)ab.GetAbilityCooldownTime();
            switch (counter)
            {
                case 1:
                    cooldown5.fillAmount = cooldownPerct;
                    break;

                case 2:
                    cooldown1.fillAmount = cooldownPerct;
                    break;

                case 3:
                    cooldown2.fillAmount = cooldownPerct;
                    break;

                case 4:
                    cooldown3.fillAmount = cooldownPerct;
                    break;

                case 5:
                    cooldown4.fillAmount = cooldownPerct;
                    break;

                default:
                    break;
            }
            counter++;

        }
    }

    void UpdateRageUI(float ragePerc)
    {
        if (rage != null)
        {
            rage.fillAmount = ragePerc;
        } 
    }

    void UpdateHealthUI(float healthPerc)
    {
        health.fillAmount = healthPerc;
    }
}
