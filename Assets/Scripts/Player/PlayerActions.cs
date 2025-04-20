using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public void TakeDamage(int damage)
    {
        StatsManager.instance.currentHealth -= damage;
        if (StatsManager.instance.currentHealth <= 0)
        {
            KillPlayer();
        }
    }

    public void HealDamage(int heal)
    {
        StatsManager.instance.currentHealth = Mathf.Clamp(
            StatsManager.instance.currentHealth + heal, 0, StatsManager.instance.maxHealth
        );
    }

    void KillPlayer()
    {
        //TO-DO Proper player removal from scene
        Destroy(gameObject);
        
    }
}
