using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip enemyDeathSound;
    [SerializeField] private AudioClip menuSound;

    [SerializeField] private AudioClip dieMusic;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.ignoreListenerPause = true;
    }

    public void JumpSFX()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void DieSFX()
    {
        audioSource.PlayOneShot(dieMusic);
    }

    public void ShootSFX()
    {
        audioSource.PlayOneShot(shootSound);
    }

    public void DamageSFX()
    {
        audioSource.PlayOneShot(damageSound);
    }

    public void PickupSFX()
    {
        audioSource.PlayOneShot(pickupSound);
    }

    public void MenuSFX()
    {
        audioSource.PlayOneShot(menuSound);
    }
    public void EnemyDeathSFX()
    {
        audioSource.PlayOneShot(enemyDeathSound);
    }
}
