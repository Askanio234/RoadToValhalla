using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public Weapon courseWeapon;

    private SpriteRenderer spriteRenderer;
    private Transform gunPos;
    private float firingRate;
    private float lastTimeFired = -2.0f;
	// Use this for initialization
	void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gunPos = gameObject.transform.Find("Gun");
        spriteRenderer.sprite = courseWeapon.image;
        firingRate = courseWeapon.rateOfFireSecs;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && isReadyToFire(Time.timeSinceLevelLoad, lastTimeFired, firingRate))
        {
            if (courseWeapon.numProjectilesInVolley == 1)
            {
                courseWeapon.Fire(gunPos);
                lastTimeFired = Time.timeSinceLevelLoad;
            } else
            {
                StartCoroutine(courseWeapon.FireInBursts(gunPos, courseWeapon.timeBetweenProjectilesInVolley));
                lastTimeFired = Time.timeSinceLevelLoad;
            }
        }
    }

    private bool isReadyToFire(float time, float lastTimeFired, float fireRate)
    {
        if (time - lastTimeFired >= fireRate)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
