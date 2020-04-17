using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private float damage;

    [SerializeField] Rigidbody rb = null;

    [SerializeField] ProjectileBehaviour ProjectileBehaviour = null;

    [SerializeField] string ownerTag = "";

    public Rigidbody RB
    {
        get { return rb; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private string sourceTag;

    public string SourceTag
    {
        get { return sourceTag; }
        set { sourceTag = value; }
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(RB.velocity);
        transform.Rotate(-90.0f, 0.0f, 0.0f);

        if(ProjectileBehaviour != null)
        {
            ProjectileBehaviour.Update();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != ownerTag)
        {
            other.gameObject.GetComponent<Damagable>().ApplyDamage(Damage);
        }
    }
}
