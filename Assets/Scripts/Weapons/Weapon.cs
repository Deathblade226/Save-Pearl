using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Weapon : MonoBehaviour
{

    public string Name { get; set; }
    public string Discription { get; set; }
    public Image WeaponImage { get; set; }
    public float Damage { get; set; }
    public float AttackSpeed { get; set; }
    public string TargetTag { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
