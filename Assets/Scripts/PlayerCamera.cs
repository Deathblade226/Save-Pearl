using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[SerializeField] Transform m_playerTransform = null;
	//[SerializeField] Camera m_camera = null;
	[SerializeField] float m_distanceFromPlayer = -12.0f;
	[SerializeField] float m_cameraYOffest = 2.0f;
	//[SerializeField][Range(0, 1)] float m_percentageOfUntrackedScreen = 0.5f;

	private void Update()
	{

		Vector3 newPosition = m_playerTransform.position;
		newPosition.z = m_distanceFromPlayer;
		newPosition.y += m_cameraYOffest;
		transform.position = newPosition;


	}

}
