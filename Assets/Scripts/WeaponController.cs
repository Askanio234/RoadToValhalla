using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public Weapon Weapon;
    
    private SpriteRenderer spriteRenderer;
    private Transform gunPos;
    private float firingRate;
    private float lastTimeFired = -2.0f;
	// Use this for initialization
	void Start () {
        Weapon = GameKeeper.gameKeeper.courseWeapon;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gunPos = gameObject.transform.Find("Gun");
        spriteRenderer.sprite = Weapon.image;
        firingRate = Weapon.rateOfFireSecs;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && isReadyToFire(Time.timeSinceLevelLoad, lastTimeFired, firingRate))
        {
            if (Weapon.numProjectilesInVolley == 1)
            {
                Weapon.Fire(gunPos);
                lastTimeFired = Time.timeSinceLevelLoad;
            } else
            {
                StartCoroutine(Weapon.FireInBursts(gunPos, Weapon.timeBetweenProjectilesInVolley));
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
