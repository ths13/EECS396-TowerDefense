using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Code;

public class Base : MonoBehaviour
{
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public Slider healthBar;
    public bool destroyed;
    public Spawn sp;

    internal void Start()
    {
        healthBar = FindObjectOfType<Slider>();
        MaxHealth = 100f;
        CurrentHealth = MaxHealth;
        healthBar.value = CalculateHealth();
        destroyed = false;
        sp = FindObjectOfType<Spawn>();
    }

    private void DealDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
        healthBar.value = CalculateHealth();
        if (CurrentHealth <= 0)
            Die();
    }

    public float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    internal void OnCollisionEnter(Collision other)
    {
        var enemyObj = other.gameObject;
        if (enemyObj.tag == "NormalEnemy")
        {
            var normEnemy = enemyObj.GetComponent<NormalEnemy>();
            DealDamage(normEnemy.AttackDamage);
        }
        else if (enemyObj.tag == "FastEnemy")
        {
            var fastEnemy = enemyObj.GetComponent<FastEnemy>();
            DealDamage(fastEnemy.AttackDamage);
        }
        else if (enemyObj.tag == "StrongEnemy")
        {
            var strongEnemy = enemyObj.GetComponent<StrongEnemy>();
            DealDamage(strongEnemy.AttackDamage);
        }
    }

    void Die()
    {
        CurrentHealth = 0;
        if (!destroyed)
        {
            Debug.Log("Base Destroyed");
            destroyed = true;
            sp.GameOver = true;
            gameObject.GetComponent<Base>().enabled = false;
        }
    }
}