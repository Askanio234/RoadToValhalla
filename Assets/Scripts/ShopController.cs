using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopController : MonoBehaviour {
    public GameObject coursePannelList;
    public GameObject itemTemplate;
    [Header("Course Weapons")]
    public Weapon[] courseWeaponsList;
    private Weapon selectedWeapon;
    private Weapon installedWeapon;
	// Use this for initialization
	void Start () {
        PopulateCourseWeapons(courseWeaponsList);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void PopulateCourseWeapons(Weapon[] weapons)
    {
        foreach (var item in weapons)
        {
            GameObject menuItem = Instantiate(itemTemplate);
            menuItem.transform.SetParent(coursePannelList.transform);
            GameObject menuItemImage = menuItem.transform.Find("ItemSprite").gameObject;
            menuItemImage.GetComponent<Image>().sprite = item.image;
            GameObject menuItemName = menuItem.transform.Find("ItemName").gameObject;
            menuItemName.GetComponent<Text>().text = item.name;

        }
    }
    public void WeaponSelected(GameObject weapon)
    {
        print(weapon);
    }
}
