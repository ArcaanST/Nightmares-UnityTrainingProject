using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider CapsuleCollider;
    bool isDead;
    bool isSinking;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        CapsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount, Vector3 Hitpoint)
    {
        if (isDead)
            return;
        enemyAudio.Play();

        currentHealth -= amount;

        hitParticles.transform.position = Hitpoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;

        CapsuleCollider.isTrigger = true;

        anim.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}
