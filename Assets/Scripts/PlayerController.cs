using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] [Range(0, 20)] float m_speed = 5.0f;
	[SerializeField] [Range(0, 20)] float m_currentSpeed = 0.0f;

	[SerializeField] [Range(0, 1)] float m_acceleration = 1.0f;

	[SerializeField] [Range(0, 1)] float m_airControl = 0.5f;
	[SerializeField] [Range(0, 1)] float m_groundDrag = 0.8f;
	[SerializeField] [Range(0, 1)] float m_landingDragMultiplier = 1.0f;
	[SerializeField] [Range(0, 30)] int m_numOfLandingDragFrames = 20;
	[SerializeField] [Range(0, 20)] float m_turnRate = 5.0f;
	[SerializeField] float m_jumpForce = 40.0f;
	[SerializeField] float m_fallGravityMultiplier = 4.0f;
	[SerializeField] [Range(0, 30)] int m_numOfJumpFrames = 10;


	[SerializeField] Transform m_groundStart = null;
	[SerializeField] Transform m_groundEnd = null;
	[SerializeField] LayerMask m_groundLayer = 0;


	Animator m_animator = null;
	Rigidbody m_rb = null;

	int m_jumpFrameCounter = 0;
	int m_landingDragCounter = 0;
	Vector3 m_storedVelocity = Vector3.zero;


	bool m_wasAirborne = false;


	void Start()
	{
		m_rb = GetComponent<Rigidbody>();
		m_animator = GetComponentInChildren<Animator>();
	}

	void Update()
	{

		
		Attack();

		MovePlayer();

		

	}

	public void Attack()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			m_animator.SetBool("RangeWeapon", true);
		}
		else
		{
			m_animator.SetBool("RangeWeapon", false);
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			m_animator.SetBool("MeleeWeapon", true);
		}
		else
		{
			m_animator.SetBool("MeleeWeapon", false);
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

			Vector3 torque = new Vector3(0, 1, 0);

			Vector3 velocity = Vector3.zero;
			velocity.x = Input.GetAxis("Horizontal");


			float animSpeed = m_rb.velocity.magnitude / m_speed;
			m_animator.SetFloat("Speed", animSpeed);


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

			float speedMultiplier = m_speed;
			if (m_currentSpeed < m_speed && velocity != Vector3.zero)//not up to speed, then we set the accelerate
			{
				m_currentSpeed += m_acceleration;
				speedMultiplier = m_currentSpeed;
			}

			m_rb.AddForce(velocity * speedMultiplier * (m_groundDrag * landingMultiplier), ForceMode.VelocityChange);

			transform.rotation = velocity != Vector3.zero ? Quaternion.LookRotation(velocity) : transform.rotation;//velocity == Vector3.zero ? transform.rotation : Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(velocity, Vector3.up), Time.deltaTime * m_turnRate);

			m_rb.velocity = Vector3.ClampMagnitude(m_rb.velocity, m_speed);


			if (Input.GetButtonDown("Jump"))
			{
				m_rb.AddRelativeForce(Vector3.up * m_jumpForce, ForceMode.VelocityChange);
				m_animator.SetBool("Jump", true);
			}

		}
		else
		{
			m_wasAirborne = true;
			Vector3 velocity = Vector3.zero;
			velocity.x = Input.GetAxis("Horizontal");

			m_rb.AddForce(velocity * m_speed * m_airControl, ForceMode.VelocityChange);
			Vector3 clamped = new Vector3(m_rb.velocity.x, 0, m_rb.velocity.z);
			clamped = Vector3.ClampMagnitude(clamped, m_speed);
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
				Debug.Log("fall");
			}

			if (Input.GetButton("Jump") && m_jumpFrameCounter < m_numOfJumpFrames)
			{
				m_jumpFrameCounter++;
				m_rb.AddRelativeForce(Vector3.up * (m_jumpForce / ((m_jumpFrameCounter * 11) - m_jumpFrameCounter * 10)), ForceMode.VelocityChange);
			}




		}
	}
}
