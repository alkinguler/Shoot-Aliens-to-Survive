using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //health mechanism
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    //stamina mechanism
    public int maxStamina = 300;
    public int currentStamina;
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;
    bool isSprinting;

    public StaminaBar staminaBar;

    public PlayerMovementController player;

    //kill stat
    public int killCount = 0;



    void Start()
    {
        if(MainMenu.SecondRun == true)
        { 
            Time.timeScale = 1f;
            MainMenu.SecondRun = false;
            Pausemenu.NeedCursor = false;
            
        }
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        
    }


    void Update()
    {
        //check isSprinting from PlayerMovementController script
        isSprinting = player.GetIsSprinting();

        //take damage if got hit by enemy

        //for testing purposes
        
     //if (Input.GetKeyDown(KeyCode.E))
     //   {
     //       TakeDamage(20);
     //       Debug.Log("You have taken damage!");
     //   }
        

        //decreasing stamina if running
        if(currentStamina > 0)
        {
            if (isSprinting)
            {
                UseStamina(1);
            }
        }
    }
    /*
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.gameObject.name);

        if (collider.gameObject.tag == "enemy")
        { 
            Rest(0.05f);
            TakeDamage(20);
           
        }
    }
    */
    
    IEnumerator Rest(float delay)
    {
        yield return new WaitForSeconds(delay);

    }
    
    public void TakeDamage(int damage)
    {
        //taken damage
        currentHealth -= damage;

        //set health bar
        healthBar.SetHealth(currentHealth);

        //die if your health is 0 or less than 0 
        if (currentHealth <= 0f)
        {
            DeathMenu.IsDead = true;
        }
    }

    public void IncreaseHealth(int increase) {
        currentHealth += increase;

        healthBar.SetHealth(currentHealth);

        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    //UI YOU ARE DEAD ANIMATION NEEDED
    public void Die()
    {
        Destroy(gameObject);
        //UI you're dead animation needed.
    }

    //spending stamina
    public void UseStamina(int amount)
    {
        if(currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.SetStamina(currentStamina);

            
            if (regen != null)
            {
                StopCoroutine(regen);
            }
            regen = StartCoroutine(RegenStamina());
            
        }
    }
    
    //regeneration of stamina
    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1);

        while ( currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            staminaBar.SetStamina(currentStamina);
            yield return regenTick;
        }
    }

}
