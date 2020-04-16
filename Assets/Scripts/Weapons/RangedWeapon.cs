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

    private float projectileMass;

    public float ProjectileMass
    {
        get { return projectileMass; }
        set { projectileMass = value; }
    }

    private Vector3 spawnPoint;

    public Vector3 SpawnPoint
    {
        get { return spawnPoint; }
        set { spawnPoint = value; }
    }

    private Vector3 targetPoint;

    public Vector3 TargetPoint
    {
        get { return targetPoint; }
        set { targetPoint = value; }
    }

    [SerializeField] private Projectile projectile = null;

    override public void OnAttack()
    {
       // Projectile p = Instantiate(projectile, spawnPoint)// need to instatiate with rotation, then add velocity
    }
}
