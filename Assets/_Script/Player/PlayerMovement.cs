using System;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    ThirdPersonCharacter m_Character;

    [SerializeField]
    private float walkMoveStopRadius = 0.2f;

    [SerializeField]
    private float attackMoveStopRedius = 5f;

    Vector3 currentDestination, clickPoint;

    private static bool isInDirectionMode = false;

    private AICharacterControl aiCharachteControl = null;
    CameraRaycaster cameraRaycaster = null;

    private const int walkableLayerNumber = 8;

    private const int enemyLayerNumber = 9;

    private GameObject walkTarget;

    void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        //m_Character = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
        aiCharachteControl = GetComponent<AICharacterControl>();
        cameraRaycaster.notifyMouseClickObservers += ProcessMaouseClick;
        Debug.Log(cameraRaycaster);

        walkTarget = new GameObject("walk Target");
    }

    private void ProcessMaouseClick(RaycastHit raycastHit, int layerHit)
    {
        switch(layerHit)
        {
            case enemyLayerNumber:
                GameObject enemy = raycastHit.collider.gameObject;
                aiCharachteControl.SetTarget(enemy.transform);
                break;
            case walkableLayerNumber:
                walkTarget.transform.position = raycastHit.point;
                aiCharachteControl.SetTarget(walkTarget.transform);
                break;
            default:
                return;
        }
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
}


