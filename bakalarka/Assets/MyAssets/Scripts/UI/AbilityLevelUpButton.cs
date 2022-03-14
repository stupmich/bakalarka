using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityLevelUpButton : MonoBehaviour
{
    [SerializeField]
    private StateManager stateManager;

    ExperienceManager xpManager;

    public GameObject player;
    public PlayerAbilities abilities;
    public PlayerAbility ability;
    public GameObject lvl1;
    public GameObject lvl2;
    public GameObject lvl3;
    public GameObject lvl4;
    public GameObject lvl5;
    public Text abilityPointsText;

    private void Start()
    {
        xpManager = ExperienceManager.instance;
        xpManager.OnAbilityPointsChanged += OnAbilityPointsChanged;
        abilities = player.GetComponent<PlayerAbilities>();

        stateManager.OnAbilitiesLoaded += OnAbilitiesLoaded;
    }
    
    public void AbilityLevelUp(Ability pAbility)
    {
        ability = abilities.getAbility(pAbility);

        if (ability == null)
        {
            if (abilities.LearnAbility(pAbility))
            {
                lvl1.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
            }
        }
        else
        {
            switch (ability.level)
            {
                case 1:
                    lvl2.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    abilities.LevelUpAbility(pAbility);
                    break;
                case 2:
                    lvl3.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    abilities.LevelUpAbility(pAbility);
                    break;
                case 3:
                    lvl4.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    abilities.LevelUpAbility(pAbility);
                    break;
                case 4:
                    lvl5.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    gameObject.GetComponent<Button>().transition = Selectable.Transition.None;
                    abilities.LevelUpAbility(pAbility);
                    break;
                default:
                    break;
            }
        }
        OnAbilityPointsChanged(xpManager.abilityPoints);
    }

    public void OnAbilitiesLoaded(PlayerAbility pPlayerAbility)
    {
        if (ability == pPlayerAbility)
        {
            switch (pPlayerAbility.level)
            {
                case 1:
                    lvl1.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    break;
                case 2:
                    lvl1.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    lvl2.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    break;
                case 3:
                    lvl1.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    lvl2.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    lvl3.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    break;
                case 4:
                    lvl1.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    lvl2.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    lvl3.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    lvl4.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    break;
                case 5:
                    lvl1.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    lvl2.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    lvl3.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    lvl4.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    lvl5.GetComponent<Image>().color = new Color32(54, 59, 209, 255);
                    gameObject.GetComponent<Button>().transition = Selectable.Transition.None;
                    break;
                default:
                    break;
            }
        }
    }

    void OnAbilityPointsChanged(int pAbilityPoints)
    {
        if (abilityPointsText != null)
        {
            abilityPointsText.text = pAbilityPoints.ToString();
        }
    }
}