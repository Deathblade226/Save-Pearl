﻿using System.Collections;
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

    private bool useGravity;

    public bool UseGravity
    {
        get { return useGravity; }
        set { useGravity = value; }
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

    override public void OnAttack(int strength, int dexterity)
    {
        Vector3 position = Input.mousePosition;
        position.z = Mathf.Abs(Camera.main.transform.position.z);
        targetPoint = Camera.main.ScreenToWorldPoint(position);
        targetPoint.z = 0.0f;
        spawnPoint = transform.position;
        Speed = 2.0f;
        Projectile p = Instantiate(projectile, spawnPoint, Quaternion.identity);
        p.transform.LookAt(targetPoint);
        p.transform.Rotate(-90.0f, 0.0f, 0.0f);
        p.RB.AddForce(((targetPoint - spawnPoint) * Speed),ForceMode.VelocityChange);
        p.OwnerTag = OwnerTag;
        p.Damage = Damage * (dexterity * 0.5f);
        //Destroy(p.gameObject, 2.0f);
        p.Lifetime = 2;
    }
    public void OnAttackMonster() {
        var player = AIUtilities.GetNearestGameObject(gameObject, "Player", 10.0f, 90.0f);
        Projectile p = Instantiate(projectile, transform.position + transform.up * 1.5f + transform.forward, Quaternion.identity);
        p.transform.LookAt(player.transform);
        p.transform.Rotate(-90.0f, 0.0f, 0.0f);
        p.RB.AddForce(transform.forward * AttackSpeed,ForceMode.VelocityChange);
        p.OwnerTag = OwnerTag;
        p.Lifetime = 2;

    }
}
