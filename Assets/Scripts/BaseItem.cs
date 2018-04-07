using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : ScriptableObject
{
    public Sprite image;
    public new string name;
    public int cost;
    public string description = "Description";
    public GameObject projectile;
    public bool isBought;
    public bool isInstalled;
}
