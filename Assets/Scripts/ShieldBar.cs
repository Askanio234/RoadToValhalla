using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour {
    public GameObject player;

    private Shield playerShield;
    private Text text;
    private string playerMaxShield;
    // Use this for initialization
    void Start () {
        text = gameObject.GetComponent<Text>();
        playerShield = player.GetComponent<Shield>();
        playerMaxShield = playerShield.maxValue.ToString();
        text.text = playerShield.GetCurrentShield().ToString() + "/" + playerMaxShield;

    }
	
	// Update is called once per frame
	void Update () {
        text.text = playerShield.GetCurrentShield().ToString() + "/" + playerMaxShield;
	}
}
