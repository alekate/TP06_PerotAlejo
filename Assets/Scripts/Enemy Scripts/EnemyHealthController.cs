using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    public Slider healthSlider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator enemyAnim;
    [SerializeField] private SoundController soundController;
    [SerializeField] private BoxCollider2D bCollider;

    public float currentHealth;

    public InitialPlayerData playerData;

    public InitialEnemyData enemyData;
    public EnemyAwareness enemyAwareness;

    private void Start()
    {
        currentHealth = enemyData.enemyHealth;
        healthSlider.maxValue = enemyData.enemyHealth;
        healthSlider.value = currentHealth;
    }

    private void Update()
    {
        if (!enemyAwareness.playerInRange)
        {
            HealthSliderOff();
        }
        else if (enemyAwareness.playerInRange && currentHealth > 0)
        {
            HealthSliderOn();
        }

        UpdateHealthSliderPosition();
    }


    private void UpdateHealthSliderPosition()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.3f);
        healthSlider.transform.position = screenPosition;
    }

    public void HealthSliderOn()
    {
        healthSlider.gameObject.SetActive(true);
    }

    public void HealthSliderOff()
    {
        healthSlider.gameObject.SetActive(false);
    }
    public void EnemyTakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth = Mathf.Max(0, currentHealth - playerData.ProjectileDamage);

            healthSlider.value = currentHealth;

            // Cambiar a rojo al ser dañado
            StartCoroutine(FlashRed());

            if (currentHealth == 0)
            {
                Die();
            }
        }
    }

    public bool IsDead() => currentHealth == 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(enemyData.enemyDamage);
        }

        if (collision.CompareTag("Projectile"))
        {
            EnemyTakeDamage();
        }
    }
    private void Die()
    {
        HealthSliderOff();
        bCollider.enabled = false;
        soundController.EnemyDeathSFX();
        StartCoroutine(StartDeathAnimation());
    }

    public IEnumerator StartDeathAnimation()
    {
        enemyAnim.SetTrigger("dead");

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }

    private IEnumerator FlashRed()
    {
        Color originalColor = spriteRenderer.color;

        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.3f);

        spriteRenderer.color = originalColor;
    }
}
