using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour
{

    [SerializeField] GameObject projectile;

    // Update is called once per frame
    abstract public void Update();
}
