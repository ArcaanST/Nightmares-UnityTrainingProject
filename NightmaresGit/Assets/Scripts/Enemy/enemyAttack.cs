using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    public float timeBetwaeenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    enemyHealth EnemyHealth;
    bool playerInRange;
    float timer;

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        EnemyHealth = GetComponent<enemyHealth>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetwaeenAttacks && playerInRange && EnemyHealth.currentHealth > 0)
        {
            Attack();
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }

    void Attack()
    {
        timer = 0f;
        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
