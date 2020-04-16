using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private float damage;

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
        if(ProjectileBehaviour != null)
        {
            ProjectileBehaviour.Update();
        }
    }
}
