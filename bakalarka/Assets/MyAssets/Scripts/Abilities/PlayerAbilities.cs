using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    #region Singleton
    public static PlayerAbilities instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public List<PlayerAbility> abilities;
    ExperienceManager xpManager;
    public Ability drinkPotionAbility;

    public event System.Action<Ability> OnAbilityLearned;

    // Start is called before the first frame update
    void Start()
    {
        abilities = new List<PlayerAbility>();
        xpManager = ExperienceManager.instance;

        LearnAbility(drinkPotionAbility);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (PlayerAbility ab in abilities)
        {
            ab.Activate(gameObject);
        }
    }

    public bool LearnAbility(Ability pAbility)
    {
        if (xpManager.abilityPoints > 0 )
        {
            PlayerAbility pa = ScriptableObject.CreateInstance<PlayerAbility>();
            pa.ability = pAbility;

            switch (abilities.Count)
            {
                case 0:
                    pa.key = KeyCode.V;
                    break;
                case 1:
                    pa.key = KeyCode.Q;
                    break;
                case 2:
                    pa.key = KeyCode.W;
                    break;
                case 3:
                    pa.key = KeyCode.E;
                    break;
                case 4:
                    pa.key = KeyCode.R;
                    break;
                default:
                    break;
            }
            pa.levelUp();

            if (abilities.Count != 0 )
            {
                xpManager.abilityPoints--;
            }

            abilities.Add(pa);

            if (OnAbilityLearned != null)
            {
                OnAbilityLearned(pAbility);
            }

            return true;
        }
        return false;
    }

    public bool LevelUpAbility(Ability pAbility)
    {
        if (xpManager.abilityPoints > 0)
        {
            foreach (PlayerAbility ab in abilities)
            {
                if (ab.ability == pAbility)
                {
                    ab.levelUp();
                    xpManager.abilityPoints--;
                    return true;
                }
            }
        }
        return false;
    }

    public PlayerAbility getAbility(Ability pAbility)
    {
        foreach (PlayerAbility ab in abilities)
        {
            if (ab.ability == pAbility)
            {
                return ab;
            }
        }

        return null;
    }
}
