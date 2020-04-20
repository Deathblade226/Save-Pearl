using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

[SerializeField] float constitution = 0;
[SerializeField] int strength = 0;
[SerializeField] int dexterity = 0;
[SerializeField] float jump = 0;
[SerializeField] float speed = 0;
[SerializeField] float armor = 0;

private void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "Player") { 

    Player player = other.gameObject.GetComponent<Player>();
    if (player != null) { 

    if (player.HealthStats.MaxHealth + constitution < 0) { player.HealthStats.MaxHealth = 1; }
    else { player.HealthStats.MaxHealth += constitution; }
    
    if (player.HealthStats.DamageReduction < 0.9f) player.HealthStats.DamageReduction += armor;

    player.Strength += strength;
    player.Dexterity += dexterity;
    
    if (player.JumpForce + jump < 2.25) { player.JumpForce = 2.25f; }
    else { player.JumpForce += jump; }

    if (player.Speed + speed < 1) { player.Speed = 1; }
    else { player.Speed += speed; }

    Destroy(gameObject);
    }

    }
}

}
