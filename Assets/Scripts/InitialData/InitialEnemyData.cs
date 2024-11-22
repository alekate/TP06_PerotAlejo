using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InitialEnemyData", menuName = "Enemy/Data", order = 1)]
public class InitialEnemyData : ScriptableObject
{

    [Header("Health")]
    public float enemyHealth = 3f; //Se muere en 3 hits

    [Header("Damage")]
    public float enemyDamage = 1f;


}
