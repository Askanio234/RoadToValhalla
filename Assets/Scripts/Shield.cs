using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    public float maxValue = 100;
    public float regenRate = 0.05f;
    public float regenDelay = 3f;

    private float currentValue;
	// Use this for initialization
	void Start () {
        currentValue = maxValue;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void RegenShield()
    {
        if (currentValue < maxValue)
        {
            currentValue += (regenRate * Time.deltaTime);
        }
    }
    
    public float AbsorbDamage(float damage)
    {
        if (currentValue > 0)
        {
            float newValue = damage - currentValue;
            if (newValue <= 0)
            {
                currentValue -= damage;
                return 0f;
            } else
            {
                currentValue = 0f;
                return newValue;
            }
        } else
        {
            return damage;
        }
    }
    public int GetCurrentShield()
    {
        return (int)currentValue;
    } 
}
