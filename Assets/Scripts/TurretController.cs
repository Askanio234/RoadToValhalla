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
        RotateTowardsMouse(Weapon.rotationSpeed);
        if (Input.GetMouseButtonDown(0) && isReadyToFire(Time.timeSinceLevelLoad, lastTimeFired, firingRate))
        {
            Weapon.Fire(gunPos);
            lastTimeFired = Time.timeSinceLevelLoad;
        }
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

    private void RotateTowardsMouse(float rotationSpeed)
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
