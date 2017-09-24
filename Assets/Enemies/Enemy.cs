using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float maxHealtPoint = 100f;

    [SerializeField]
    float radiusAttack = 4f;

    [SerializeField]
    AICharacterControl ai;

    private ThirdPersonCharacter thirPersonCharacter = null;

    private GameObject player;

    float currentHealtPoint = 100f;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        thirPersonCharacter = GetComponent<ThirdPersonCharacter>();
        print(thirPersonCharacter);
    }


    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if(distanceToPlayer <= radiusAttack)
        {
            ai.target = player.transform;
        } else
        {
            ai.target = transform;
        }
    }

    public float healthAsPercentage
    {
        get
        {
            return currentHealtPoint / maxHealtPoint;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        //Draw attack
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radiusAttack);
    }

}
