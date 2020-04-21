using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickup : MonoBehaviour {

[SerializeField] Weapon loot = null;
[SerializeField] WeaponHolder m_weapons = null;

private int index = 0;

private void Start() {
    index = Random.Range(0, m_weapons.weapons.Count + 1);
    loot = Instantiate(m_weapons.weapons[index], gameObject.transform);
}
private void OnTriggerEnter(Collider other) {
        
    PlayerController player = other.GetComponent<PlayerController>();

    if (player != null && player.PlayerStats != null) { 

    if (loot.GetComponent<MeleeWeapon>() != null) { 

    int newIndex = player.PlayerStats.m_meleeWeaponIndex;
    player.PlayerStats.m_meleeWeaponIndex = index;
    index = newIndex;
    
    Weapon newWeapon = player.PlayerStats.MeleeWeapon;
    player.PlayerStats.MeleeWeapon = (MeleeWeapon)loot;
    loot = newWeapon;
    
    } else if (loot.GetComponent<RangedWeapon>() != null) { 

    int newIndex = player.PlayerStats.m_rangedWeaponIndex;
    player.PlayerStats.m_rangedWeaponIndex = index;
    index = newIndex;
    
    Weapon newWeapon = player.PlayerStats.RangedWeapon;
    player.PlayerStats.RangedWeapon = (RangedWeapon)loot;
    loot = newWeapon;


    } else { Debug.Log("This item isnt a weapon"); }

    }

}

}
