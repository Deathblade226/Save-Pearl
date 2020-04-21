using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickup : MonoBehaviour {

[SerializeField] Weapon loot = null;
[SerializeField] WeaponHolder m_weapons = null;

private int index = 0;

private void Start() {
    index = Random.Range(0, m_weapons.weapons.Count);
    loot = Instantiate(m_weapons.weapons[index], gameObject.transform);
}
private void OnTriggerEnter(Collider other) {
        
    PlayerController player = other.GetComponent<PlayerController>();

    if (player != null) { 

    if (loot.GetComponent<MeleeWeapon>() != null) { 

    int newIndex = player.PlayerStats.m_meleeWeaponIndex;
    player.PlayerStats.m_meleeWeaponIndex = index;
    index = newIndex;
    
    Weapon newWeapon = player.PlayerStats.MeleeWeapon;
    Weapon oldLoot = loot;
    Destroy(player.PlayerStats.MeleeWeapon.gameObject);
    Destroy(loot.gameObject);
    player.PlayerStats.MeleeWeapon = Instantiate((MeleeWeapon)oldLoot, player.LeftHand);
    loot = Instantiate(newWeapon, gameObject.transform);
    player.m_meleeRenderer = player.PlayerStats.MeleeWeapon.GetComponentInChildren<Renderer>();

    } else if (loot.GetComponent<RangedWeapon>() != null) { 

    int newIndex = player.PlayerStats.m_rangedWeaponIndex;
    player.PlayerStats.m_rangedWeaponIndex = index;
    index = newIndex;
    
    Weapon newWeapon = player.PlayerStats.RangedWeapon;
    Weapon oldLoot = loot;
    Destroy(player.PlayerStats.RangedWeapon.gameObject);
    Destroy(loot.gameObject);
    player.PlayerStats.RangedWeapon = Instantiate((RangedWeapon)oldLoot, player.LeftHand);
    loot = Instantiate(newWeapon, gameObject.transform);
	player.m_rangedRenderer = player.PlayerStats.RangedWeapon.GetComponentInChildren<Renderer>();

    } else { Debug.Log("This item isnt a weapon"); }

    }

}

}
