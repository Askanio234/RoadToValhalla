using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKeeper : MonoBehaviour {
    public static GameKeeper gameKeeper;
    public ShopController shopController;
    public int energyCredits = 1000;
    public Weapon courseWeapon;
    // array should hold n+1 levels
    public int maxLelvelReached = 0;

    void Awake()
        //singleton pattern
    {
        if (gameKeeper == null)
        {
            gameKeeper = this;
        } else if (gameKeeper != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
	// Use this for initialization
	void Start () {
        courseWeapon = shopController.selectedWeapon as Weapon;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GainCredits(int amount)
    {
        energyCredits += amount;
    }

    public void SpendCredits(int amount)
    {
        //protect from negative values
        if (amount > energyCredits)
        {
            energyCredits = 0;
        }
        else
        {
            energyCredits -= amount;
        }
    }
}
