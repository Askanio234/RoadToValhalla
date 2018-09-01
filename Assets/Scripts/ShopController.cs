using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ShopController : MonoBehaviour {


    public GameObject descriptionPannel;
    public GameObject creditsPannel;
    public BaseItem selectedCourseWeapon;
    public BaseItem selectedTurret;

    private BaseItem installedWeapon;
    private GameObject[] templates;
	// Use this for initialization
	void Start () {
        templates = GameObject.FindGameObjectsWithTag("ItemTemplate");
        UpdateDescriptionPanel(selectedCourseWeapon);
        UpdateCreditsPannel();
	}
	
	// Update is called once per frame
	void Update () {
        
    }


    public void CourseWeaponSelected(GameObject button)
    {
        selectedCourseWeapon = button.GetComponent<ShopTemplateRender>().item;
        UpdateDescriptionPanel(selectedCourseWeapon);
    }

    public void TurretSelected(GameObject button)
    {
        selectedTurret = button.GetComponent<ShopTemplateRender>().item;
        UpdateDescriptionPanel(selectedTurret);
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

    public void installTurret(GameObject itemTemplate)
    {
        BaseItem weapon = itemTemplate.GetComponent<ShopTemplateRender>().item;
        if (!weapon.isBought)
        {
            BuyItem(weapon);
        }
        GameKeeper.gameKeeper.turretWeapon.isInstalled = false;
        weapon.isInstalled = true;
        GameKeeper.gameKeeper.turretWeapon = weapon as Weapon;
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
