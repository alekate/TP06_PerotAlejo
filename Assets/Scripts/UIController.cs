using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PointsSystem pointsSystem;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private InitialPlayerData playerData;
    [SerializeField] private Animator UI1animator;

    [Header("GameUI")]
    [SerializeField] private TextMeshProUGUI pointUI;
    [SerializeField] private TextMeshProUGUI vialsUI;
    [SerializeField] private Image currentHealthBar;

    [Header("AudioSetting")]
    [SerializeField] private AudioMixer myMixer;

    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider MenuSlider;

    [Header("UI")]
    private bool isPaused = false;
    private bool UI1IsVisible = true;
    public GameObject pauseUI;
    public GameObject gameUI;
    public GameObject gameUI1;

    private void Start()
    {
        UpdateVials();
        UpdateHealthBar();
        UpdatePoints();

        //Sound stuff
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
        SetMenuVolume();

        UI1animator.SetBool("IsVisible", UI1IsVisible);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                pauseUI.SetActive(true);
                isPaused = true;
                gameUI.SetActive(false);
            }
            else
            {
                Time.timeScale = 1f;
                pauseUI.SetActive(false);
                isPaused = false;
                gameUI.SetActive(true);

            }
        }

        if (Input.GetKeyDown(playerData.UIKey))
        {
            UI1IsVisible = !UI1IsVisible;

            UI1animator.SetBool("IsVisible", UI1IsVisible);
        }
    }

    public void UpdateHealthBar()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 4;
    }
    public void UpdatePoints()
    {
        pointUI.text = pointsSystem.currentPoints.ToString();
    }

    public void UpdateVials()
    {
        vialsUI.text = playerHealth.currentHealingVials.ToString();
    }

    public void SetMasterVolume()
    {
        float volume = MasterSlider.value;
        myMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }
    public void SetMusicVolume()
    {
        float volume = MusicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
    public void SetMenuVolume()
    {
        float volume = MenuSlider.value;
        myMixer.SetFloat("Menu", Mathf.Log10(volume) * 20);
    }

}
