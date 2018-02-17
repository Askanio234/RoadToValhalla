using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public Weapon courseWeapon;

    private SpriteRenderer spriteRenderer;
    private Transform gunPos;
    private float firingRate;
	// Use this for initialization
	void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gunPos = gameObject.transform.Find("Gun");
        spriteRenderer.sprite = courseWeapon.image;
        firingRate = courseWeapon.rateOfFireSecs;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            courseWeapon.Fire(gunPos);
        }
    }
}
