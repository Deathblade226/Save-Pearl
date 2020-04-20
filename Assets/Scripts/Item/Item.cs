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

    player.HealthStats.MaxHealth += constitution;
    player.HealthStats.DamageReduction += armor;
    player.Strength += strength;
    player.Dexterity += dexterity;
    player.JumpForce += jump;
    player.Speed += speed;

    }

    }
}

}
