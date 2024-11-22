using System.Collections;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float activationDistance = 5f;
    public GameObject target;

    [Header("Enemy Components")]
    [SerializeField] private InitialEnemyData enemyData;
    [SerializeField] private EnemyHealthController healthController;
    [SerializeField] private BoxCollider2D enemyCollider;
    [SerializeField] private Animator enemyAnim;

    public bool playerInRange = false; 

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        if (!playerInRange)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToPlayer <= activationDistance)
            {
                TriggerSpawn();
            }

            if (healthController.currentHealth < enemyData.enemyHealth)
            {
                TriggerSpawn();
                healthController.HealthSliderOn();
            }
        }
    }

    private void TriggerSpawn()
    {
        playerInRange = true;
        enemyCollider.enabled = true; 
        StartCoroutine(StartSpawnAnimation());

        healthController.HealthSliderOn(); 
    }

    private IEnumerator StartSpawnAnimation()
    {
        enemyAnim.SetTrigger("spawn");

        yield return new WaitForSeconds(0.5f);

        gameObject.GetComponent<EnemyMovementController>().enabled = true;

        healthController.healthSlider.gameObject.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // Rango de detección
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }
}
