using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] float damageInterval = 1;

    [SerializeField] private float damage;

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    float timeElapsed = 0;

    private void OnTriggerStay(Collider other)
    {
        timeElapsed += Time.deltaTime;

        if(timeElapsed >= damageInterval && other.gameObject.tag != "Untagged")
        {
            timeElapsed = 0;
            other.gameObject.GetComponent<Damagable>().ApplyDamage(Damage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timeElapsed = 0;
    }

}
