using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{
    [SerializeField] private UIController UI;

    public float currentPoints;

    private void Awake()
    {
        currentPoints = PlayerPrefs.GetFloat("CurrentPoints", 0); 
        UI.UpdatePoints(); 
    }

    public void AddPoints(float points)
    {
        currentPoints += points;
        PlayerPrefs.SetFloat("CurrentPoints", currentPoints);
        UI.UpdatePoints(); 
    }

    public void ResetPoints()
    {
        // Reiniciar puntos actuales
        currentPoints = 0;
        PlayerPrefs.SetFloat("CurrentPoints", currentPoints); 
        UI.UpdatePoints();
    }
}
