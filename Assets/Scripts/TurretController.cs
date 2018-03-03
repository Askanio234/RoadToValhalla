using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {
    public Weapon Weapon;

    private SpriteRenderer spriteRenderer;
    private Transform gunPos;
    private float firingRate;
    private float lastTimeFired = -2.0f;
    // Use this for initialization
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gunPos = gameObject.transform.Find("Gun");
        spriteRenderer.sprite = Weapon.image;
        firingRate = Weapon.rateOfFireSecs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool isReadyToFire(float time, float lastTimeFired, float fireRate)
    {
        if (time - lastTimeFired >= fireRate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
