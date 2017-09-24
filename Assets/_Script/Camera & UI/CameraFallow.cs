using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform camera;
	
	// Update is called once per frame
	void LateUpdate ()
    {
        camera.position = player.position;
	}
}
