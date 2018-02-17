using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon", menuName ="Weapons")]
public class Weapon : ScriptableObject {

    public Sprite image;
    public WeaponSlot weaponSlot;
    public new string name;
    public int cost;
    public float rateOfFireSecs;
    public float clipCount;
    public float reloadTime;
    public GameObject projectile;
    public int numProjectilesInVolley = 1;
    public float timeBetweenProjectilesInVolley;
    public float projectileSpeed = 10f;


    public void Fire(Transform gunPos)
    {
        for (int i = 0; i <= numProjectilesInVolley; i++)
        {
            GameObject shot = Instantiate(projectile, gunPos.position, Quaternion.identity);
            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        }
    }


    

}

public enum WeaponSlot { Course, Turret }
