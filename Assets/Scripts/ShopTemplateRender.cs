using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTemplateRender : MonoBehaviour {
    [Header("Render item: ")]
    public BaseItem item;
    public GameObject actionButton;

	// Use this for initialization
	void Start () {
        RenderItem();
	}
	
    void RenderItem()
    {
        GameObject menuItemImage = gameObject.transform.Find("ItemSprite").gameObject;
        menuItemImage.GetComponent<Image>().sprite = item.image;
        GameObject menuItemName = gameObject.transform.Find("ItemName").gameObject;
        menuItemName.GetComponent<Text>().text = item.name;
        RenderActionButtonText(actionButton, item);
    }

    public void RenderActionButtonText(GameObject actionButon, BaseItem item)
    {
        string installbuttonText;
        if (item.isInstalled)
        {
            installbuttonText = "Installed";
            actionButton.GetComponent<Button>().interactable = false;
        }
        else if (item.isBought && !item.isInstalled)
        {
            installbuttonText = "Install";
            actionButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            if (requestMoney(item.cost)){
                installbuttonText = "Buy";
                actionButton.GetComponent<Button>().interactable = true;
            } else
            {
                installbuttonText = "Not enough energy";
                actionButon.GetComponent<Button>().interactable = false;
            }
            
        }
        actionButton.transform.Find("Text").GetComponent<Text>().text = installbuttonText;
        //debugging stuff
        //print(item + " installed: " + item.isInstalled + " is bought: " + item.isBought + " button text should be:" + installbuttonText);
        //print("But in fact " + actionButton.transform.Find("Text").GetComponent<Text>().text);
    }

    private bool requestMoney(int amount)
    {
        if (amount <= GameKeeper.gameKeeper.energyCredits)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
}
