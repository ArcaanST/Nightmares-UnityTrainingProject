using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetwaeenBullets = 0.15f;
    public float range = 100f;

    float timer;
    Ray shootRay;
    RaycastHit shoothit;
    int shoottableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

    private void Awake()
    {
        shoottableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(Input.GetButton("Fire1") && timer >= timeBetwaeenBullets)
        {
            Shoot();
        }

        if(timer >= timeBetwaeenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;

    }

    void Shoot()
    {
        timer = 0f;
        gunAudio.Play();
        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        
        if(Physics.Raycast (shootRay, out shoothit, range, shoottableMask))
        {
            enemyHealth EnemyHealth = shoothit.collider.GetComponent<enemyHealth>();
            if(EnemyHealth != null)
            {
                EnemyHealth.TakeDamage(damagePerShot, shoothit.point);
            }
            gunLine.SetPosition(1, shoothit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
