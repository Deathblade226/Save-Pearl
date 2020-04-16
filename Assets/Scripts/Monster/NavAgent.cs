using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgent : MonoBehaviour {

[SerializeField] NavMeshAgent m_navAgent = null;
[SerializeField] float MoveCd = 5.0f;
[SerializeField] float searchDistance = 5.0f;
[SerializeField] float attackCD = 3.0f;
[SerializeField] float attackRange = 2.0f;
[SerializeField] Animator animator = null;
[SerializeField] bool m_x = true;
[SerializeField] bool m_z = true;

private float MoveTime = 0;
private float AttackTime = 0;
private bool Attacking = false;

void Update() {
	if (m_navAgent.isOnNavMesh) { 
	var player = AIUtilities.GetNearestGameObject(gameObject, "Player", 10.0f, 90.0f);
	//Debug.Log($"Player: {player}");
	if (player != null) { 
	Attacking = ((transform.position - player.transform.position).magnitude <= attackRange && AttackTime <= 0);
	//Debug.Log($"Attacking: {Attacking}");
	if (Attacking) { animator.SetTrigger("Attack");  AttackTime = attackCD; m_navAgent.isStopped = true; }
	else if ((transform.position - player.transform.position).magnitude <= attackRange) { m_navAgent.isStopped = true; }
	else { m_navAgent.SetDestination(player.transform.position); m_navAgent.isStopped = false; }
	
	} else if (MoveTime <= 0) {
	m_navAgent.isStopped = false;
	//Debug.Log("Moving");
	MoveTime = MoveCd;
	Vector3 target = Vector3.up;
	if (m_x && m_z) target = new Vector3(gameObject.transform.position.x + Random.Range(-searchDistance, searchDistance), 0, gameObject.transform.position.z + Random.Range(-searchDistance, searchDistance));
	else if (!m_x && m_z) target = new Vector3(transform.position.x, 0, gameObject.transform.position.z + Random.Range(-searchDistance, searchDistance));
	else if (m_x && !m_z) target = new Vector3(gameObject.transform.position.x + Random.Range(-searchDistance, searchDistance), 0, transform.position.z);

	if (target != null && m_navAgent != null && m_navAgent.isOnNavMesh) { m_navAgent.SetDestination(target); }

	}

	if (MoveTime > 0) { MoveTime -= Time.deltaTime; }
	if (AttackTime > 0) { AttackTime -= Time.deltaTime; }

	animator.SetFloat("Speed", m_navAgent.velocity.magnitude);
	}
}

}
