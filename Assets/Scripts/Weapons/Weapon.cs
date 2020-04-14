using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string weaponName;

    public string Name
    {
        get { return weaponName; }
        set { weaponName = value; }
    }

    [SerializeField] private string description;

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    [SerializeField] private Sprite weaponImage;

    public Sprite WeaponImage
    {
        get { return weaponImage; }
        set { weaponImage = value; }
    }

    [SerializeField] private float damage;

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    [SerializeField] private float attackSpeed;

    public float AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }

    [SerializeField] private string ownerTag;

    public string OwnerTag
    {
        get { return ownerTag; }
        set { ownerTag = value; }
    }

    [SerializeField] private float attackDelay;

    public float AttackDelay
    {
        get { return attackDelay; }
        set { attackDelay = value; }
    }

    abstract public void OnAttack();
    

}
