using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float damage;

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

    [SerializeField] ProjectileBehaviour ProjectileBehaviour = null;

    // Update is called once per frame
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
        if (other.gameObject.tag != ownerTag && other.gameObject.tag != "Untagged")
        {
            Debug.Log(other.gameObject.tag);
            other.gameObject.GetComponent<Damagable>().ApplyDamage(Damage);
            Destroy(gameObject);
        }
    }
}
