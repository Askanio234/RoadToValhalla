using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public int health = 100;
    private int maxHealth;
	// Use this for initialization
	void Start () {
        maxHealth = health;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void HealDamage(int ammount)
    {
        health += ammount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
