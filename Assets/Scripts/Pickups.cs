using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] private PointsSystem pointsSystem;
    [SerializeField] private SoundController soundController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            pointsSystem.AddPoints(1);
            soundController.PickupSFX();
        }
    }
}
