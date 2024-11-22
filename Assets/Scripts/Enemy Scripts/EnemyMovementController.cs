using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float movementDistance = 5f;

    private float leftEdge;
    private float rightEdge;
    private bool movingLeft = true;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    public void Update()
    {
        MoveEnemy();
    }

    public void MoveEnemy()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                transform.localScale = Vector3.one;
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                movingLeft = true;
            }
        }
    }
}
