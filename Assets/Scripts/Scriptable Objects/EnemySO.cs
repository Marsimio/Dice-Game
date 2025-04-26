using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public int speed;
    public GameObject[] itemsToDrop;
}
