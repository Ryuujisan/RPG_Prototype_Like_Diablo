using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    ThirdPersonCharacter m_Character;

    [SerializeField]
    CameraRaycaster cameraRaycaster;

    [SerializeField]
    private float walkMoveStopRadius = 0.2f;

    [SerializeField]
    private float attackMoveStopRedius = 5f;

    Vector3 currentDestination, clickPoint;

    private static bool isInDirectionMode = false;

    private void Start()
    {
        //cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        //m_Character = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            isInDirectionMode = !isInDirectionMode;
            currentDestination = transform.position;
        }

        if(isInDirectionMode)
        {
            ProcessDirectionMove();
        }
    }


    private void ProcessDirectionMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");



        Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;


        m_Character.Move(m_Move, false, false);
    }


        //private void ProcessMouseMovement()
     //{
     //    if (Input.GetMouseButton(0))
     //    {
     //        clickPoint = cameraRaycaster.hit.point;
     //        switch (cameraRaycaster.currentLayerHit)
     //        {
    //            case Layer.Walkable:
     //                currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);
     //                break;
    //            case Layer.Enemy:
     //                currentDestination = ShortDestination(clickPoint, attackMoveStopRadius);
     //                break;
     //            default:
    //                print("Unexpected layer found");
     //                return;
     //        }
     //    }
     //    WalkToDestination();
     //}


    private void WalkToDestination()
    {
        var playerToClickPoint = currentDestination - transform.position;
        if (playerToClickPoint.magnitude >= 0)
        {
            m_Character.Move(playerToClickPoint, false, false);
        }
        else
        {
            m_Character.Move(Vector3.zero, false, false);
        }
    }

    private Vector3 ShortDestination(Vector3 destination, float shotering)
    {
        Vector3 redictionVector = (destination - transform.position).normalized * shotering;
        return destination - redictionVector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, clickPoint);

        Gizmos.DrawSphere(currentDestination, 0.15f);
        Gizmos.DrawSphere(clickPoint, 0.15f);

        //Draw attack
        Gizmos.color = new Color(255f, 0.0f, 0.0f, .5f);
        Gizmos.DrawWireSphere(transform.position, attackMoveStopRedius);
    }
}


