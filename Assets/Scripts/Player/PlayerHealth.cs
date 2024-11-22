using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private InitialPlayerData playerData;
    [SerializeField] private SoundController soundController;
    [SerializeField] private GameObject music;
    [SerializeField] private UIController UI;
    [SerializeField] private Animator playerDamageAnim;
    [SerializeField] private Image currentHealthBar;
    private bool isInvulnerable = false;
    private float invulnerabilityCooldown = 1f;

    public float currentHealth;
    private bool isDead = false;

    public float currentHealingVials;

    public GameObject gameUI;
    public GameObject deathUI;

    private void Awake()
    {
        currentHealth = playerData.playerHealth;
        currentHealingVials = PlayerPrefs.GetFloat("CurrentHealingVials", 0);
        UI.UpdateHealthBar();
        UI.UpdateVials();
        playerDamageAnim = transform.Find("Player Sprite").GetComponent<Animator>();
    }

    private void Update()
    {
        UI.UpdateHealthBar();

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Dead();
            currentHealth = 0;
        }

        if (isDead && Input.GetKey(playerData.shootKey))
        {
            RestartScene();
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable) return;

        if (currentHealth > 0)
        {
            soundController.DamageSFX();
            currentHealth -= damage;
            playerDamageAnim.SetTrigger("hurt");

            UI.UpdateHealthBar();
        }
    }

    private IEnumerator ApplyInvulnerabilityCooldown()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityCooldown);
        isInvulnerable = false;
    }

    public void Heal()
    {
        if (currentHealth > 0 && currentHealth < 4 && currentHealingVials > 0)
        {
            soundController.DamageSFX();
            currentHealth += playerData.playerHealPoints;

            currentHealingVials = currentHealingVials - 1;
            PlayerPrefs.SetFloat("CurrentHealingVials", currentHealingVials);


            UI.UpdateHealthBar();
            UI.UpdateVials();
        }
    }

    private void Dead()
    {
        deathUI.SetActive(true);
        gameUI.SetActive(false);
        soundController.DieSFX();
        music.gameObject.SetActive(false);
        playerDamageAnim.SetTrigger("dead");

        GetComponent<PlayerController>().enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero; 
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void RestartScene()
    {
        deathUI.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
