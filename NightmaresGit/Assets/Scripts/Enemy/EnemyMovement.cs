using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    NavMeshAgent nav;
    PlayerHealth playerH;
    enemyHealth enemyH;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerH = GameObject.Find("Player").GetComponent<PlayerHealth>();
        nav = GetComponent<NavMeshAgent>();
        enemyH = GetComponent<enemyHealth>();

    }

    private void Update()
    {
        if(enemyH.currentHealth > 0 && playerH.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
        
    }
}