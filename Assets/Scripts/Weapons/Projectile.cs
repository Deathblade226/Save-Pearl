using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField] Rigidbody rb = null;

    [SerializeField] ProjectileBehaviour ProjectileBehaviour = null;

    [SerializeField] string ownerTag = "";

    [SerializeField] float lifetime = 10; //need to set up the destroy after x time

    public Rigidbody RB
    {
        get { return rb; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public string OwnerTag
    {
        get { return ownerTag; }
        set { ownerTag = value; }
    }

    public float Lifetime 
    {
        get => lifetime;
        set
        {
            lifetime = value;
            Destroy(this.gameObject, lifetime);
        }
    }

    void Update()
    {

        if(RB.velocity != Vector3.zero) transform.rotation = Quaternion.LookRotation(RB.velocity);
        transform.Rotate(-90.0f, 0.0f, 0.0f);

        if (ProjectileBehaviour != null)
        {
            ProjectileBehaviour.Update();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != OwnerTag)
        {
            Damagable damagable = other.gameObject.GetComponent<Damagable>();

            if (damagable != null) { other.gameObject.GetComponent<Damagable>().ApplyDamage(Damage); 
            Destroy(gameObject);
            }

        }
    }
}
