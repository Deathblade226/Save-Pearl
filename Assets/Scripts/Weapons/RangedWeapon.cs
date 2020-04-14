using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{

    private float speed;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private float lifetime;

    public float LifeTime
    {
        get { return lifetime; }
        set { lifetime = value; }
    }

    private Vector3 spawnPoint;

    public Vector3 SpawnPoint
    {
        get { return spawnPoint; }
        set { spawnPoint = value; }
    }

    private float dropOff;

    public float DropOff
    {
        get { return dropOff; }
        set { dropOff = value; }
    }

    override public void OnAttack()
    {

    }
}
