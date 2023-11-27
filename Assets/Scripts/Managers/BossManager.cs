using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    [SerializeField] private string bossName;
    [SerializeField] private float maxHealth;
    private float currentHealth;
    [SerializeField] private float damage;
    [SerializeField] private Image healthBar;
    [SerializeField] TextMeshProUGUI txtHealth;
    private void Start() {
        currentHealth = maxHealth;
        txtHealth.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }
    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;
        if(currentHealth < 0) currentHealth = 0;
        healthBar.fillAmount = currentHealth/maxHealth;
        txtHealth.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }
    public float GetDamage()
    {
        return damage;
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
