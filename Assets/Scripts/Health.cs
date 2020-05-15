using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Health Instance { get; private set; }

    [SerializeField] private int maxHealth;
    [SerializeField] private float invincibleLenght;

    private float invincibleCounter;
    private SpriteRenderer spriteRenderer;
    public int currentHealth { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if (invincibleCounter <= 0)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                    spriteRenderer.color.b, 1f);
            }
        }
            
    }

    public void DealDamage(int amount = 1)
    {
        if (invincibleCounter <= 0)
        {
            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                gameObject.SetActive(false);
            }
            else
            {
                invincibleCounter = invincibleLenght;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                    spriteRenderer.color.b, .5f);

                PlayerController.Instance.KnockBack();
            }

            UIController.Instance.UpdateHealthDisplay(currentHealth);
        }
    }
}
