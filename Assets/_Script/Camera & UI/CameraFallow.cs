using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    [SerializeField]
    private Transform camera;

    [SerializeField]
    private float timeLerp = 2f;

    Transform player;

    private void Start()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        player = playerGameObject.transform;
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        camera.position = Vector3.Lerp(camera.position, player.position, timeLerp * Time.deltaTime);
	}
}
