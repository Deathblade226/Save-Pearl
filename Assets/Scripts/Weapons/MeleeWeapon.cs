using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{


    [SerializeField] Collider WeaponCollider = null;

    public float timer = 0.0f;

    private int playerStrength = 0;

    public bool canAttack { get { return Timer >= (AttackSpeed + AttackDelay); } }

    public float Timer { get => timer; set => timer = value; }

    private void Start()
    {
        Timer = AttackSpeed + AttackDelay;
    }
    private void Update()
    {
        if (Timer < AttackSpeed + AttackDelay)
        {
            Timer += 0.025f;
        }
    }

    override public void OnAttack(int strength, int dexterity)
    {
        if (canAttack)
        {
            Timer = 0;
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
