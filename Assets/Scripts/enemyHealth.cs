using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour {
    public float enemyMaxHealth;
    float currentHealth;
    public GameObject enemyDeathFX;
    public Slider enemySlider;

    public bool drops;
    public GameObject theDrop;
    public AudioClip deathKnell;

	// Use this for initialization
	void Start () {
        currentHealth = enemyMaxHealth;
        enemySlider.maxValue = currentHealth;
        enemySlider.value = currentHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addDamage(float damage)
    {
        enemySlider.gameObject.SetActive(true);
        currentHealth = currentHealth - damage;
        enemySlider.value = currentHealth;

        if (currentHealth <= 0) makeDead();
    }
     public void makeDead()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathKnell, transform.position);
        Instantiate(enemyDeathFX, transform.position, transform.rotation);
        Destroy(transform.root.gameObject);
        if (drops) Instantiate(theDrop, transform.position, transform.rotation);
    }
}
