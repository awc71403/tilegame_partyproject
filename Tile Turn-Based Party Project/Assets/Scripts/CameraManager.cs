using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Target")]
	public PlayerManager player;

	[Header("Camera Follow Settings")]
	[Range(0f, 30f)]
	public float maxFollowSpeed;
	[Range(0f, 10f)]
	public float smoothTime;

    private Transform charTransform;

    [Header("Camera Conditions (Do not modify these fields through Editor)")]
	public Vector2 currentSpeed;

	public void Start() {
		maxFollowSpeed = 20.00f;
		smoothTime = 0.1f;
	}
	/**
	 * Update() function will be called (automatically) by Unity engine every frame. Normally, you should
	 * add any gameplay-related codes here (eg. input detection, NPC's ai logic and etc). Here we are
	 * dealing with basic camera controls.
	 * Note that the access token (public/protected/private) does not matter if you want it to be
	 * automatically called by the engine.
	 */
	private void Update() {
        player = PlayerManager.singleton;
        if (player) {
            charTransform = player.gameObject.transform;
            Vector2 currentPosition = Vector2.SmoothDamp(transform.position, charTransform.position, ref currentSpeed, smoothTime, maxFollowSpeed); // Follow the player.
            transform.position = new Vector3(currentPosition.x, currentPosition.y, transform.position.z); // We should not modify the z axis of the camera.
        }
	}
}
