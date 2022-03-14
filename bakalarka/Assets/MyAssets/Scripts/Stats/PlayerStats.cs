using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    #region Singleton


    public static PlayerStats instance;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    #endregion

    ExperienceManager xpManager;

    public int rage;
    public int maxRage = 100;

    public Stat vitality;
    public Stat strength;
    public Stat dexterity;
    public Stat gold;
    public Stat blockChance;
    public Stat critChance;
    public Stat attackSpeed;

    public event System.Action<float> OnRageChanged;
    public event System.Action OnStatChanged;
    

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        xpManager = ExperienceManager.instance;
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment pNewItem, Equipment pOldItem)
    {
        if (pNewItem != null)
        {
            armor.AddModifier(pNewItem.armorModifier);
            damage.AddModifier(pNewItem.damageModifier);
            vitality.AddModifier(pNewItem.vitalityModifier);
            strength.AddModifier(pNewItem.strengthModifier);
            dexterity.AddModifier(pNewItem.dexterityModifier);
            blockChance.AddModifier(pNewItem.blockChanceModifier);

            blockChance.AddModifier(pNewItem.armorModifier / 10);
            critChance.AddModifier(pNewItem.strengthModifier);
            attackSpeed.AddModifier(pNewItem.dexterityModifier);
            damage.AddModifier(pNewItem.strengthModifier + pNewItem.dexterityModifier);
        }

        if (pOldItem != null)
        {
            armor.RemoveModifier(pOldItem.armorModifier);
            damage.RemoveModifier(pOldItem.damageModifier);
            vitality.RemoveModifier(pOldItem.vitalityModifier);
            strength.RemoveModifier(pOldItem.strengthModifier);
            dexterity.RemoveModifier(pOldItem.dexterityModifier);
            blockChance.RemoveModifier(pOldItem.blockChanceModifier);

            blockChance.RemoveModifier(pOldItem.armorModifier / 10);
            critChance.RemoveModifier(pOldItem.strengthModifier);
            attackSpeed.RemoveModifier(pOldItem.dexterityModifier);
            damage.RemoveModifier(pOldItem.strengthModifier + pOldItem.dexterityModifier);
        }
    }

    public void AddStrength()
    {
        if (xpManager.statPoints > 0 )
        {
            strength.AddStatPoint();
            critChance.AddStatPoint();
            damage.AddStatPoint();

            xpManager.UseStatPoint();

            if (OnStatChanged != null)
            {
                OnStatChanged();
            }
        }
    }

    public void AddDexterity()
    {
        if (xpManager.statPoints > 0)
        {
            dexterity.AddStatPoint();
            attackSpeed.AddStatPoint();
            damage.AddStatPoint();

            xpManager.UseStatPoint();

            if (OnStatChanged != null)
            {
                OnStatChanged();
            }
        }
    }

    public void AddVitality()
    {
        if (xpManager.statPoints > 0)
        {
            vitality.AddStatPoint();
            xpManager.UseStatPoint();

            maxHealth += 10;

            if (OnStatChanged != null)
            {
                OnStatChanged();
            }
        }
    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }

    public void AddModifierStrength(int modifier)
    {
        Debug.Log(modifier);

        strength.AddModifier(modifier);

        damage.AddModifier(modifier);
        critChance.AddModifier(modifier);

        if (OnStatChanged != null)
        {
            OnStatChanged();
        }
    }

    public void AddModifierVitality(int modifier)
    {
        vitality.AddModifier(modifier);
        maxHealth += modifier * 10;

        if (OnStatChanged != null)
        {
            OnStatChanged();
        }
    }

    public void AddModifierDexterity(int modifier)
    {
        dexterity.AddModifier(modifier);
        damage.AddModifier(modifier);
        attackSpeed.AddModifier(modifier);

        if (OnStatChanged != null)
        {
            OnStatChanged();
        }
    }

    public void AddModifierArmor(int modifier)
    {
        armor.AddModifier(modifier);
        blockChance.AddModifier(modifier / 10);

        if (OnStatChanged != null)
        {
            OnStatChanged();
        }
    }

    public void RemoveModifierStrength(int modifier)
    {
        strength.RemoveModifier(modifier);
        damage.RemoveModifier(modifier);
        critChance.RemoveModifier(modifier);

        if (OnStatChanged != null)
        {
            OnStatChanged();
        }
    }

    public void RemoveModifierVitality(int modifier)
    {
        vitality.RemoveModifier(modifier);
        maxHealth -= modifier * 10;

        if (OnStatChanged != null)
        {
            OnStatChanged();
        }
    }

    public void RemoveModifierDexterity(int modifier)
    {
        dexterity.RemoveModifier(modifier);
        damage.RemoveModifier(modifier);
        attackSpeed.RemoveModifier(modifier);

        if (OnStatChanged != null)
        {
            OnStatChanged();
        }
    }

    public void RemoveModifierArmor(int modifier)
    {
        armor.RemoveModifier(modifier);
        blockChance.RemoveModifier(modifier / 10);

        if (OnStatChanged != null)
        {
            OnStatChanged();
        }
    }

    public int GetRage()
    {
        return rage;
    }

    public void SetRage(int pRage)
    {
        rage += pRage;

        float ragePercent = rage / (float)maxRage;

        if (OnRageChanged != null)
        {
            OnRageChanged(ragePercent);
        }
    }

    public void ApplyVitalityModifiers()
    {
        maxHealth += vitality.GetValue();
    }
}
