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

    private void OnCollisionStay(Collision collision)
    {
        timeElapsed += Time.deltaTime;

        if(timeElapsed >= damageInterval)
        {
            timeElapsed = 0;
            collision.gameObject.GetComponent<Damagable>().ApplyDamage(Damage);
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        timeElapsed = 0;
    }

}
