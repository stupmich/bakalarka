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
    [SerializeField]
    private StateManager stateManager;

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

        stateManager.OnAbilitiesLoaded += OnAbilitiesLoaded;
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

    public void OnAbilitiesLoaded(PlayerAbility pPlayerAbility)
    {
        switch (pPlayerAbility.key)
        {
            case KeyCode.Q:
                ability1.sprite = pPlayerAbility.ability.abilityImage;
                break;
            case KeyCode.W:
                ability2.sprite = pPlayerAbility.ability.abilityImage;
                break;
            case KeyCode.E:
                ability3.sprite = pPlayerAbility.ability.abilityImage;
                break;
            case KeyCode.R:
                ability4.sprite = pPlayerAbility.ability.abilityImage;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        foreach (PlayerAbility ab in abilities.abilities)
        {
            float cooldownPerct = ab.GetCurrentCooldownTime() / (float)ab.GetAbilityCooldownTime();
            switch (ab.key)
            {
                case KeyCode.Q:
                    cooldown1.fillAmount = cooldownPerct;
                    break;
                case KeyCode.W:
                    cooldown2.fillAmount = cooldownPerct;
                    break;
                case KeyCode.E:
                    cooldown3.fillAmount = cooldownPerct;
                    break;
                case KeyCode.R:
                    cooldown4.fillAmount = cooldownPerct;
                    break;
                case KeyCode.V:
                    cooldown5.fillAmount = cooldownPerct;
                    break;
                default:
                    break;
            }
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
