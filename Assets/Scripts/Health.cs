using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public int currentHealth = 100;
    public int maxHealth;
	// Use this for initialization
	void Start () {
        maxHealth = currentHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void HealDamage(int ammount)
    {
        currentHealth += ammount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
