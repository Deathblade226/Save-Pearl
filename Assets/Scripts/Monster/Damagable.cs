using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

[SerializeField] float m_health = 100;
[SerializeField] Damage m_damage = null;
[SerializeField] [Range(-1,1)]float m_damageReduction = 0;

private float maxHealth;

public float MaxHealth { get => maxHealth; set => maxHealth = value; }
public float health { get => m_health; set => m_health = value; }
public bool destroyed { get; set; } = false;

private void Start() { MaxHealth = health; }

public void ApplyDamage(float damageAmount) {
	health = health - (damageAmount - (damageAmount*m_damageReduction));
	if (!destroyed && health <= 0) {
	Destroy(gameObject);
	destroyed = true;

	if (m_damage != null) {
	Damage damage = Instantiate(m_damage, transform.position, Quaternion.identity);
	damage.Spawn(transform.position, Vector3.zero, Vector3.up);
	}
}
	
}

}
