using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTemplateRender : MonoBehaviour {
    [Header("Render item: ")]
    public BaseItem item;

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
    }
}
