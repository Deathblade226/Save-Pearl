using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	//[SerializeField] [Range(0, 20)] float m_speed = 2.0f;
	[SerializeField] [Range(0, 20)] float m_currentSpeed = 0.0f;
	[SerializeField] [Range(0, 1)] float m_acceleration = 1.0f;

	[SerializeField] [Range(0, 1)] float m_airControl = 0.5f;
	[SerializeField] [Range(0, 1)] float m_groundDrag = 0.8f;
	[SerializeField] [Range(0, 1)] float m_landingDragMultiplier = 1.0f;
	[SerializeField] [Range(0, 30)] int m_numOfLandingDragFrames = 20;
	//[SerializeField] float m_jumpForce = 2.25f;
	[SerializeField] float m_fallGravityMultiplier = 4.0f;
	[SerializeField] [Range(0, 30)] int m_numOfJumpFrames = 10;

	[SerializeField] Transform m_groundStart = null;
	[SerializeField] Transform m_groundEnd = null;
	[SerializeField] LayerMask m_groundLayer = 0;

	Renderer m_meleeRenderer = null;
	Renderer m_rangedRenderer = null;
	[SerializeField] Transform m_rightHand = null;
	[SerializeField] Transform m_leftHand = null;

	[SerializeField] Player m_playerStats = null;
	[SerializeField] WeaponHolder m_weapons = null;

	public Player PlayerStats { get; }




	Animator m_animator = null;
	Rigidbody m_rb = null;

	int m_jumpFrameCounter = 0;
	int m_landingDragCounter = 0;

	int m_weaponNumber = 0; //0 = melee, 1 = ranged

	bool m_wasAirborne = false;
	bool m_isBowDrawn = false;
	bool m_isMidAttack = false;


	void Start()
	{
		m_rb = GetComponent<Rigidbody>();
		m_animator = GetComponentInChildren<Animator>();
		m_playerStats = new Player(tag.ToString());
		m_playerStats.MeleeWeapon = Instantiate((MeleeWeapon)m_weapons.weapons[m_playerStats.m_meleeWeaponIndex], m_rightHand);
		m_playerStats.RangedWeapon = Instantiate((RangedWeapon)m_weapons.weapons[m_playerStats.m_rangedWeaponIndex], m_leftHand);
		//m_playerStats.MeleeWeapon.transform.SetParent(m_hand);
		m_meleeRenderer = m_playerStats.MeleeWeapon.GetComponentInChildren<Renderer>();
		m_rangedRenderer = m_playerStats.RangedWeapon.GetComponentInChildren<Renderer>();
		if (m_playerStats.MeleeWeapon != null)
		{
			m_playerStats.MeleeWeapon.OwnerTag = tag;
		}
		if (m_playerStats.RangedWeapon != null)
		{
			m_playerStats.RangedWeapon.OwnerTag = tag;
		}
		if(m_playerStats.HealthStats == null)
		{
			m_playerStats.HealthStats = GetComponent<Damagable>();
			m_playerStats.HealthStats.health = m_playerStats.m_health;
			m_playerStats.HealthStats.DamageReduction = m_playerStats.m_damageReduction;
		}
	}

	void Update()
	{
		m_playerStats.m_health = m_playerStats.HealthStats.health;
		m_playerStats.m_damageReduction = m_playerStats.HealthStats.DamageReduction;
		
		Attack();

		MovePlayer();

		

	}

	public void AttackFinished()
	{
		m_isMidAttack = false;
		m_isBowDrawn = false;
		m_animator.SetBool("MeleeWeapon", false);

	}

	public void BowDrawn()
	{
		m_isBowDrawn = true;
	}

	public void Attack()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if(!m_isMidAttack && !m_isBowDrawn)
			{
				m_weaponNumber++;
				m_weaponNumber = (m_weaponNumber) % 2;
			}
			
		}
		if (m_weaponNumber == 0 && !m_isMidAttack)
		{
			m_meleeRenderer.enabled = true;
			m_rangedRenderer.enabled = false;
			if (Input.GetMouseButton(0))
			{
				if (m_playerStats.MeleeWeapon.canAttack)
				{
					m_animator.SetBool("MeleeWeapon", true);
					m_playerStats.MeleeWeapon.OnAttack(m_playerStats.Strength, m_playerStats.Dexterity);
					m_isMidAttack = true;
				}
			}
			if (!Input.GetMouseButton(0))
			{
				m_animator.SetBool("MeleeWeapon", false);
			}
		}
		else if (m_weaponNumber == 1)
		{
			m_meleeRenderer.enabled = false;
			m_rangedRenderer.enabled = true;
			if (Input.GetMouseButton(0) && !m_isMidAttack)
			{
				m_animator.SetBool("RangeWeapon", true);
				m_isMidAttack = true;
			}
			if (!Input.GetMouseButton(0) && m_isBowDrawn)
			{
				m_playerStats.RangedWeapon.OnAttack(m_playerStats.Strength, m_playerStats.Dexterity);
				m_animator.SetBool("RangeWeapon", false);
				AttackFinished();
			}
		}


	}

	public void MovePlayer()
	{
		Debug.DrawLine(m_groundStart.position, m_groundEnd.position);
		Vector3 direction = m_groundEnd.position - m_groundStart.position;
		bool onGround = Physics.Raycast(m_groundStart.position, direction, out RaycastHit hit, direction.magnitude, m_groundLayer);
		m_animator.SetBool("OnGround", onGround);


		if (onGround)
		{
			m_animator.SetBool("Jump", false);
			m_animator.SetBool("Fall", false);

			m_jumpFrameCounter = 0;

			Vector3 velocity = Vector3.zero;
			velocity.x = Input.GetAxis("Horizontal");


			float animSpeed = m_rb.velocity.magnitude / m_playerStats.Speed;
			m_animator.SetFloat("Speed", animSpeed < 0.01f ? 0.0f : animSpeed);


			if (m_wasAirborne)
			{
				m_landingDragCounter++;
				m_wasAirborne = false;
			}
			float landingMultiplier = 1;

			if (m_landingDragCounter > 0)
			{
				m_landingDragCounter++;
				if (m_landingDragCounter > m_numOfLandingDragFrames)
					m_landingDragCounter = 0;
				landingMultiplier = m_landingDragMultiplier;
			}
			if (velocity == Vector3.zero)
			{
				m_rb.velocity *= m_groundDrag * 0.5f;
				m_currentSpeed = m_acceleration;
			}

			float speedMultiplier = m_playerStats.Speed;
			if (m_currentSpeed < m_playerStats.Speed && velocity != Vector3.zero)//not up to speed, then we set the accelerate
			{
				m_currentSpeed += m_acceleration;
				speedMultiplier = m_currentSpeed;
			}

			m_rb.AddForce(velocity * speedMultiplier * (m_groundDrag * landingMultiplier), ForceMode.VelocityChange);

			if(!m_isBowDrawn && !m_isMidAttack)
			{
				if(Vector3.Dot(velocity, Vector3.right) > 0.0f)
				{
					transform.rotation = Quaternion.LookRotation(Vector3.right);
				} 
				else if(Vector3.Dot(velocity, Vector3.right) < 0.0f)
				{
					transform.rotation = Quaternion.LookRotation(Vector3.left);
				}
				 //velocity == Vector3.zero ? transform.rotation : Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(velocity, Vector3.up), Time.deltaTime * m_turnRate);
			}
			else if(m_isBowDrawn)
			{
				Vector3 position = Input.mousePosition;
				position.z = Mathf.Abs(Camera.main.transform.position.z);
				Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(position);
				transform.rotation = mouseInWorld.x > transform.position.x ? Quaternion.LookRotation(Vector3.right) : Quaternion.LookRotation(Vector3.left);

			}
			m_rb.velocity = Vector3.ClampMagnitude(m_rb.velocity, m_playerStats.Speed);


			if (Input.GetButtonDown("Jump"))
			{
				m_rb.AddRelativeForce(Vector3.up * m_playerStats.JumpForce, ForceMode.VelocityChange);
				m_animator.SetBool("Jump", true);
			}

		}
		else
		{
			m_wasAirborne = true;
			Vector3 velocity = Vector3.zero;
			velocity.x = Input.GetAxis("Horizontal");

			m_rb.AddForce(velocity * m_playerStats.Speed * m_airControl, ForceMode.VelocityChange);
			Vector3 clamped = new Vector3(m_rb.velocity.x, 0, 0);
			clamped = Vector3.ClampMagnitude(clamped, m_playerStats.Speed);
			clamped.y = m_rb.velocity.y;
			m_rb.velocity = clamped;


			if (m_rb.velocity.y < 0.2f)
			{
				Vector3 v = m_rb.velocity;
				v.y += (Physics.gravity.y * m_fallGravityMultiplier) * Time.deltaTime;
				m_rb.velocity = v;
			}

			if (m_rb.velocity.y < 0.0f && m_animator.GetCurrentAnimatorStateInfo(0).IsName("Falling Idle") == false)
			{
				m_animator.SetBool("Fall", true);
			}

			if (Input.GetButton("Jump") && m_jumpFrameCounter < m_numOfJumpFrames)
			{
				m_jumpFrameCounter++;
				m_rb.AddRelativeForce(Vector3.up * (m_playerStats.JumpForce / ((m_jumpFrameCounter * 11) - m_jumpFrameCounter * 10)), ForceMode.VelocityChange);
			}




		}
	}
}
