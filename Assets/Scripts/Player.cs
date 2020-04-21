using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
	[System.NonSerialized]
	Damagable m_healthStats = null;
	public float m_health = 100;
	public float m_damageReduction = 0;

	[System.NonSerialized]
	MeleeWeapon m_meleeWeapon = null;
	[System.NonSerialized]
	RangedWeapon m_rangedWeapon = null;

	public int m_meleeWeaponIndex = 0;
	public int m_rangedWeaponIndex = 2;
	float m_jumpForce = 2.25f;
	float m_speed = 2.0f;
	float m_armor = 0.0f;

	string tag = null;

	public Player(float health, float damageReduction, int meleeIndex, int rangedIndex, float jumpForce, float speed, float armor, int strength, int dexterity, string tag)
	{
		m_health = health;
		m_damageReduction = damageReduction;
		m_meleeWeaponIndex = meleeIndex;
		m_rangedWeaponIndex = rangedIndex;
		JumpForce = jumpForce;
		Speed = speed;
		m_armor = armor;
		m_dexterity = dexterity;
		m_strength = strength;
		this.tag = tag;
	}

	public Player()
	{
		m_health = 100;
		m_damageReduction = 0;
		m_meleeWeaponIndex = 0;
		m_rangedWeaponIndex = 2;
		JumpForce = 2.25f;
		Speed = 2.0f;
		m_armor = 0.0f;
		m_dexterity = 5;
		m_strength = 5;
		this.tag = "";
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
