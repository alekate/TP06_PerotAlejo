using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InitialPlayerData", menuName = "Player/Data", order = 1)]
public class InitialPlayerData : ScriptableObject
{
    [Header("PlayerInput")]
    public KeyCode jumpKey = KeyCode.UpArrow;
    public KeyCode leftKey = KeyCode.LeftArrow;
    public KeyCode rightKey = KeyCode.RightArrow;
    public KeyCode downKey = KeyCode.DownArrow;

    public KeyCode shootKey = KeyCode.Z;
    public KeyCode healKey = KeyCode.C;
    public KeyCode UIKey = KeyCode.LeftShift;
    
    [Header("ProjectileStuff")]
    public float ProjectileDamage = 1;

    [Header("JumpHeight")]
    public float jumpForce;
    public float maxJumpTime;

    [Header("AirControl")]
    public float airControlFactor;

    [Header("MovementSpeed")]
    public float movementSpeed;
    public float maxSpeed = 10f;

    [Header("Health")]
    public float playerHealth = 4f;
    public float playerHealPoints = 1f; 
}
