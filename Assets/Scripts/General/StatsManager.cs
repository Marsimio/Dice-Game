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
    
    [Header("Movement Stats")]
    public int speed;
    public float jumpHeight;
    
    [Header("Health Stats")]
    public int maxHealth;
    public int currentHealth;

    [Header("Utility Stats")] 
    public int money;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
