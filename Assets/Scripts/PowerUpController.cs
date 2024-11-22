using System.Collections;
using UnityEditor;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private float duration = 10f; // Duración de los efectos
    [SerializeField] private GameObject spriteRenderer;
    [SerializeField] private SoundController soundController;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private InitialPlayerData playerData;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private UIController UI;
    [SerializeField] private GameObject shieldObject;

    [Header("Power-Up Types")]
    [SerializeField] private bool healPickup = false;
    [SerializeField] private bool damageBoostPickup = false;
    [SerializeField] private bool invulnerabilityPickup = false;

    [Header("Damage Boost Settings")]
    [SerializeField] private float damageMultiplier = 2f;
    private bool isPowerUpActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPowerUpActive)
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            PlayerAttack playerAttack = collision.GetComponent<PlayerAttack>();

            soundController.PickupSFX();
            StartCoroutine(ActivatePowerUp(playerController, playerAttack));
           
        }
    }

    public IEnumerator ActivatePowerUp(PlayerController playerController, PlayerAttack playerAttack)
    {
        isPowerUpActive = true;

        spriteRenderer.SetActive(false);

        if (healPickup)
        {
            playerHealth.currentHealingVials = playerHealth.currentHealingVials + 1;

            PlayerPrefs.SetFloat("CurrentHealingVials", playerHealth.currentHealingVials);

            UI.UpdateVials();
        }

        else if (damageBoostPickup)
        {
            float originalDamage = 1f;

            playerData.ProjectileDamage *= damageMultiplier;

            yield return new WaitForSeconds(duration);

            playerData.ProjectileDamage = originalDamage;
            
        }

        else if (invulnerabilityPickup)
        {
            float originalHealth = 4f;

            playerData.playerHealth = 10000000000000000000000000000f; //Hardcodeo :p

            playerController.hasTripleJump = true;

            ShowShield(true);

            yield return new WaitForSeconds(duration);

            playerData.playerHealth = originalHealth;

            playerController.hasTripleJump = false;

            ShowShield(false);
        }

        Destroy(gameObject);
    }

    private void ShowShield(bool isActive)
    {
        if (shieldObject == null) return;

        shieldObject.SetActive(isActive);

        if (isActive)
        {
            SpriteRenderer shieldRenderer = shieldObject.GetComponent<SpriteRenderer>();

            Color color = shieldRenderer.color;
            color.a = 0.5f;
            shieldRenderer.color = color;
        }
        else
        {
            SpriteRenderer shieldRenderer = shieldObject.GetComponent<SpriteRenderer>();

            if (shieldRenderer != null)
            {
                Color color = shieldRenderer.color;
                color.a = 0f; 
                shieldRenderer.color = color;
            }
        }
    }
}
