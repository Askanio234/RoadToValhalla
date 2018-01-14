using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public GameObject player;

    private Health playerHealth;
    private float fillAmmount;
    private Image content;
	// Use this for initialization
	void Start () {
        content = gameObject.GetComponent<Image>();
        playerHealth = player.GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
        HandleBar();
	}

    void HandleBar()
    {
        fillAmmount = (float)playerHealth.currentHealth / (float)playerHealth.maxHealth;
        content.fillAmount = fillAmmount;
    }
}
