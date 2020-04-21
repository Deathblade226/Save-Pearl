using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	[SerializeField] Damagable m_healthStats = null;
	public float m_health = 100;
	public float m_damageReduction = 0;

	[SerializeField] MeleeWeapon m_meleeWeapon = null;
	[SerializeField] RangedWeapon m_rangedWeapon = null;
	public int m_meleeWeaponIndex = 0;
	public int m_rangedWeaponIndex = 2;
	[SerializeField] float m_jumpForce = 2.25f;
	[SerializeField] [Range(0, 20)] float m_speed = 2.0f;
	[SerializeField] [Range(-1, 1)] float m_armor = 0.0f;

	string tag = null;

	public Player(string tag)
	{
		this.tag = tag;
	}

	private int m_strength = 5;
	private int m_dexterity = 5;

	public MeleeWeapon MeleeWeapon { get => m_meleeWeapon; set { m_meleeWeapon = value; m_meleeWeapon.OwnerTag = this.tag; } }
	public RangedWeapon RangedWeapon { get => m_rangedWeapon; set { m_rangedWeapon = value; m_rangedWeapon.OwnerTag = this.tag; } }
	public int Strength { get => m_strength; set => m_strength = value; }
	public int Dexterity { get => m_dexterity; set => m_dexterity = value; }
	public float JumpForce { get => m_jumpForce; set => m_jumpForce = value; }
	public float Speed { get => m_speed; set => m_speed = value; }
	public Damagable HealthStats { get => m_healthStats; set => m_healthStats = value; }
}
