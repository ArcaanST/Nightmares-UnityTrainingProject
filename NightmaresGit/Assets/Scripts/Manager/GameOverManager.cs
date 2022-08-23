using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float restartDelay = 5f;
    public string cena1;

    Animator anim;
    float restartTimer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");

            restartTimer += Time.deltaTime;

            if(restartTimer >= restartDelay)
            {
                SceneManager.LoadScene(cena1);
            }
        }
    }
}
