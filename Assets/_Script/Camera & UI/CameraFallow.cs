using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    [SerializeField]
    private Transform camera;

    Transform player;

    private void Start()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        player = playerGameObject.transform;
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        camera.position = player.position;
	}
}
