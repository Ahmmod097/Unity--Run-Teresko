using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

    public float fullHealth;
    float currentHealth;
    playerController controlMovement;
    public GameObject deathFX;
    public AudioClip playerHurt;
    public AudioClip playerDeathSound;
    AudioSource playerAS;

    public restartGame theGameManager;
    //HUD Variables
    public Slider healthSlider;
    public Image damageScreen;
    public Text gameOverScreen;
    public Text winGameScreen;
    bool damaged = false;
    Color damagedColour = new Color(0f, 0f, 0f, 1.0f);
    float smoothColour = 5f;

	// Use this for initialization
	void Start () {
        currentHealth = fullHealth;
        controlMovement =GetComponent<playerController>();

        //HUD Initialization
        healthSlider.maxValue = fullHealth;
        healthSlider.value = fullHealth;
        damaged = false;
        playerAS = GetComponent<AudioSource>();

        }
	
	// Update is called once per frame
	void Update () {
        if (damaged)
        {
            damageScreen.color = damagedColour;
        }
        else
        {
            damageScreen.color = Color.Lerp(damageScreen.color,Color.clear,smoothColour*Time.deltaTime);
        }
        damaged = false;
	}

    public void addDamage(float damage)
    {
        if (damage <= 0) return;
        currentHealth -= damage;
        playerAS.clip = playerHurt;
        playerAS.Play();
        //playerAS.PlayOneShot(playerHurt);

        healthSlider.value = currentHealth;
        damaged = true;
        if (currentHealth <= 0)
        {
            makeDead();
        }
        
    }

    public void addHealth(float healthAmount)
    {
        currentHealth += healthAmount;
        if (currentHealth > fullHealth) currentHealth = fullHealth;
        healthSlider.value = currentHealth;
    }

    public void makeDead()
    {
        GameObject effect = Instantiate(deathFX,transform.position,transform.rotation);
        Destroy(gameObject);
        Destroy(healthSlider.gameObject);
        AudioSource.PlayClipAtPoint(playerDeathSound, transform.position);
        damageScreen.color = damagedColour;
        Animator gameOverAnimator = gameOverScreen.GetComponent<Animator>();
        gameOverAnimator.SetTrigger("gameOver");
        theGameManager.restartTheGame();
    }
    public void winGame()
    {
        Destroy(gameObject);
        theGameManager.restartTheGame();
        Animator winGameAnimator = winGameScreen.GetComponent<Animator>();
        winGameAnimator.SetTrigger("gameOver");
    }
}
