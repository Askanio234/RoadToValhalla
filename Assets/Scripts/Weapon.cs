using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Weapon", menuName ="Weapons")]
public class Weapon : BaseItem
{

    //public Sprite image;
    public WeaponSlot weaponSlot;
    //public new string name;
    //public int cost;
    public float rateOfFireSecs;
    public float clipCount;
    public float reloadTime;
    //public GameObject projectile;
    public int numProjectilesInVolley = 1;
    public float timeBetweenProjectilesInVolley;
    public float projectileSpeed = 10f;
    public float rotationSpeed = 5f;

    private Vector2 direction;

    public IEnumerator FireInBursts(Transform gunPos, float time)
    {
        for (int i = 1; i <= numProjectilesInVolley; i++)
        {
            GameObject shot = Instantiate(projectile, gunPos.position, gunPos.rotation);
            shot.GetComponent<Rigidbody2D>().AddForce(shot.transform.up * projectileSpeed);
            yield return new WaitForSeconds(time);
        }
    }

    public void Fire(Transform gunPos)
    {
        GameObject shot = Instantiate(projectile, gunPos.position, gunPos.rotation);
        Rigidbody2D shotRB = shot.GetComponent<Rigidbody2D>();
        shotRB.velocity = gunPos.parent.parent.GetComponent<Rigidbody2D>().velocity;
        shotRB.AddForce(shot.transform.up * projectileSpeed);
    }

    

}

public enum WeaponSlot { Course, Turret }
