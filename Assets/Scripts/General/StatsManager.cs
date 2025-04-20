using System;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;
    
    [Header("Combat Stats")]
    public float attackDistance;
    public float attackDelay;
    public float attackSpeed;
    public int attackDamage;
    public float jumpHeight;
    
    [Header("Movement Stats")]
    public int speed;
    
    [Header("Health Stats")]
    public int maxHealth;
    public int currentHealth;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
}
