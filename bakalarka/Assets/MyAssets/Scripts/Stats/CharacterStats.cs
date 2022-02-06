using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public bool died = false;

    public int maxHealth = 100;
    public int currentHealth { get; private set; } //kazda trieda moze get, set iba tato

    public Stat damage;
    public Stat armor;

    public event System.Action<float> OnHealthChanged;
    public event System.Action OnDeath;

    public virtual void Start()
    {
        currentHealth = maxHealth;
    }

    void Awake ()
    {
        
    }

    void Update()
    {

    }

    public void TakeDamage(int pDamage)
    {
        pDamage -= armor.GetValue();
        pDamage = Mathf.Clamp(pDamage, 0, int.MaxValue);//vrati danu hodnotu ak je v intervale 

        currentHealth -= pDamage;

        float healthPercent = currentHealth / (float)maxHealth;

        if (OnHealthChanged != null)
        {
            OnHealthChanged(healthPercent);
        }

        if (currentHealth <= 0 && died == false)
        {
            if (OnDeath != null)
            {
                OnDeath();
            }
            died = true;

            Die();
        }
    }

   
    public virtual void Die()
    {
    }

    public void Heal(int health)
    {
        if (currentHealth + health > maxHealth)
        {
            int exceededHealth = currentHealth + health - maxHealth;
            currentHealth += (health - exceededHealth);
        } else
        {
            currentHealth += health;
        }

        if (OnHealthChanged != null)
        {
            float healthPercent = currentHealth / (float)maxHealth;
            OnHealthChanged(healthPercent);
        }
    }
}
