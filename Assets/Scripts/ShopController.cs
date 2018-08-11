using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ShopController : MonoBehaviour {


    public GameObject descriptionPannel;
    public GameObject creditsPannel;
    public BaseItem selectedWeapon;

    private BaseItem installedWeapon;
    private GameObject[] templates;
	// Use this for initialization
	void Start () {
        templates = GameObject.FindGameObjectsWithTag("ItemTemplate");
        UpdateDescriptionPanel(selectedWeapon);
        UpdateCreditsPannel();
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
        GameObject costText = descriptionPannel.transform.Find("CostText").gameObject;
        costText.GetComponent<Text>().text = "Cost: " + item.cost + " energy credits";
        GameObject statsText = descriptionPannel.transform.Find("StatsText").gameObject;
        if (item.GetType() == typeof(Weapon))
        {
            statsText.GetComponent<Text>().text = "Damage: " + item.projectile.GetComponent<Projectile>().damage.ToString();
        }
    }

    public void RenderAllButtons(GameObject[] templates)
    {
        foreach (GameObject template in templates)
        {
            BaseItem item = template.GetComponent<ShopTemplateRender>().item;
            GameObject actionButton = template.GetComponent<ShopTemplateRender>().actionButton;
            template.GetComponent<ShopTemplateRender>().RenderActionButtonText(actionButton, item);
        }
    }

    public void installCourseWeapon(GameObject itemTemplate)
    {
        BaseItem weapon = itemTemplate.GetComponent<ShopTemplateRender>().item;
        if (!weapon.isBought)
        {
            BuyItem(weapon);
        }
        GameKeeper.gameKeeper.courseWeapon.isInstalled = false;
        weapon.isInstalled = true;
        GameKeeper.gameKeeper.courseWeapon = weapon as Weapon;
        //Need to update all buttons
        RenderAllButtons(templates);
        UpdateCreditsPannel();
    }

    

    private void UpdateCreditsPannel()
    {
        creditsPannel.GetComponent<Text>().text = GameKeeper.gameKeeper.energyCredits.ToString();
    }

    private void BuyItem(BaseItem item)
    {
        GameKeeper.gameKeeper.SpendCredits(item.cost);
        item.isBought = true;
    }
}
