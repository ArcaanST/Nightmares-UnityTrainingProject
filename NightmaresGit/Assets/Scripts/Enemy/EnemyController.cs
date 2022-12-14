using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float playerDistance;
    public float awareAI = 10f;
    public float AIMoveSpeed;
    public float damping = 6.0f;

    public Transform[] navPoint;
    public UnityEngine.AI.NavMeshAgent agent;
    public int DestPoint = 0;
    public Transform goal;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = goal.position;

        agent.autoBraking = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(player.position, transform.position);

        if(playerDistance < awareAI)
        {
            LookAtPlayer();
            Debug.Log("Seen");
        }

        if(playerDistance < awareAI)
        {
            if (playerDistance > 2f)
                Chase();
            else
                GotoNextPoint();

                
        }
        {
            if (agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }

        void LookAtPlayer()
        {
            transform.LookAt(player);
        }

        void GotoNextPoint()
        {
            if (navPoint.Length == 0)
                return;
            agent.destination = navPoint[DestPoint].position;
            DestPoint = (DestPoint + 1) % navPoint.Length;
        }

        void Chase()
        {
            transform.Translate(Vector3.forward * AIMoveSpeed * Time.deltaTime);
        }
    }
}
