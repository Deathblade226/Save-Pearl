using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] Damagable m_healthStats = null;
	[SerializeField] MeleeWeapon m_meleeWeapon = null;
	[SerializeField] RangedWeapon m_rangedWeapon = null;
	[SerializeField] float m_jumpForce = 2.25f;
	[SerializeField] [Range(0, 20)] float m_speed = 2.0f;
	[SerializeField] [Range(-1, 1)] float m_armor = 0.0f;


	private int m_strength = 5;
	private int m_dexterity = 5;

	public MeleeWeapon MeleeWeapon { get => m_meleeWeapon; set => m_meleeWeapon = value; }
	public RangedWeapon RangedWeapon { get => m_rangedWeapon; set => m_rangedWeapon = value; }
	public int Strength { get => m_strength; set => m_strength = value; }
	public int Dexterity { get => m_dexterity; set => m_dexterity = value; }
	public float JumpForce { get => m_jumpForce; set => m_jumpForce = value; }
	public float Speed { get => m_speed; set => m_speed = value; }
	public Damagable HealthStats { get => m_healthStats; set => m_healthStats = value; }
}
