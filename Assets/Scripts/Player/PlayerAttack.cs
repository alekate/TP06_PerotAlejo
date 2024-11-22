using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private InitialPlayerData playerData;
    [SerializeField] private Animator playerAttackAnim;
    [SerializeField] private SoundController soundController;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab; 

    [Header("Timers")]
    public float attackCooldown = 1f; 
    private float cooldownTimer;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

        playerAttackAnim = transform.Find("Player Sprite").GetComponent<Animator>();
    }

    private void Update()
    {

        cooldownTimer += Time.deltaTime;

        if (Input.GetKeyDown(playerData.shootKey) && cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        cooldownTimer = 0; 
        playerAttackAnim.SetTrigger("attack");
        soundController.ShootSFX();

        float direction = transform.localScale.x > 0 ? 1f : -1f;

        Vector3 spawnPosition = transform.position + new Vector3(0, -2f, 0);


        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = newProjectile.GetComponent<Projectile>();
        projectileScript.SetDirection(direction); 
    }
}
