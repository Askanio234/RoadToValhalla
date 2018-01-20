using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public float currentHealth = 100f;
    public float maxHealth;
	// Use this for initialization
	void Start () {
        maxHealth = currentHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void GetDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void HealDamage(float ammount)
    {
        currentHealth += ammount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
