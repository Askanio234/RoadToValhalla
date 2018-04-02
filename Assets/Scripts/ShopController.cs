using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using System;

public class ShopController : MonoBehaviour {


    public GameObject descriptionPannel;

    public BaseItem selectedWeapon;
    private Weapon installedWeapon;
	// Use this for initialization
	void Start () {
        UpdateDescriptionPanel(selectedWeapon);
	}
	
	// Update is called once per frame
	void Update () {
        
    }


    public void WeaponSelected(GameObject button)
    {
        selectedWeapon = button.GetComponent<ShopTemplateRender>().item;
        UpdateDescriptionPanel(selectedWeapon);
    }

    private void UpdateDescriptionPanel(BaseItem item)
    {
        GameObject nameText = descriptionPannel.transform.Find("Name").gameObject;
        nameText.GetComponent<Text>().text = item.name;
        GameObject descriptionText = descriptionPannel.transform.Find("Description").gameObject;
        descriptionText.GetComponent<Text>().text = item.description;
        GameObject statsText = descriptionPannel.transform.Find("StatsText").gameObject;
        if (item.GetType() == typeof(Weapon))
        {
            statsText.GetComponent<Text>().text = "Damage: " + item.projectile.GetComponent<Projectile>().damage.ToString();
        }
    }
}
