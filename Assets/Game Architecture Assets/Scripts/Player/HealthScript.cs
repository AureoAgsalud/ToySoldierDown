using UnityEngine;
using System;

public class HealthScript : MonoBehaviour
{
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    public event Action<float>  OnHealthChanged;
    public event Action OnDeath;
    [SerializeField] FloatingHealthBar healthBar;
    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();  
    }
    private void Start()
    {
        currentHealth = maxHealth;
        bool isScubscribed = (OnHealthChanged != null);

        if(isScubscribed)
        {
            OnHealthChanged.Invoke(currentHealth);
        }

        
    }
    public void TakeDamage(float amount)
    {
        //- Reduce Health by amount
        currentHealth -= amount;
        
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    //public void RestoreHealth(float amount)
    //{
    //    currentHealth += amount;
    //    if (currentHealth > maximumHealth)
    //    {
    //        currentHealth = maximumHealth;
    //    }
    //}

    public void Die()
    {
        OnDeath?.Invoke();
        Debug.Log("u ded bro");
    }
}
