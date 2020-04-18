using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{


    [SerializeField] Collider WeaponCollider = null;

    private float timer = 0;

    private int playerStrength = 0;

    public bool canAttack { get { return timer >= AttackSpeed + AttackDelay; } }

    private void Start()
    {
        timer = AttackSpeed + AttackDelay;
    }
    private void Update()
    {
        if (timer < AttackSpeed + AttackDelay)
        {
            timer += Time.deltaTime;
        }
    }

    override public void OnAttack(int strength, int dexterity)
    {
        if (canAttack)
        {
            timer = 0;
            playerStrength = strength;
            WeaponCollider.enabled = true;
            StartCoroutine("Attacking");

        }
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(AttackSpeed - 0.5f);
        WeaponCollider.enabled = false;
        StopCoroutine("Attacking");
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag != OwnerTag && other.gameObject.tag != "Untagged")
        {
            other.gameObject.GetComponent<Damagable>().ApplyDamage(Damage * (playerStrength * 0.5f));

        }
    }
}
