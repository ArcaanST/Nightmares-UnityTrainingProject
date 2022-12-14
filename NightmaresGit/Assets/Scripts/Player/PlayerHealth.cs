using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource PlayerAudio;
    PlayerMovement playerMov;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        PlayerAudio = GetComponent<AudioSource>();
        playerMov = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }

        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        PlayerAudio.Play ();
        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();
        anim.SetTrigger("Die");

        PlayerAudio.clip = deathClip;
        PlayerAudio.Play();

        playerMov.enabled = false;
        playerShooting.enabled = false;
    }
}
