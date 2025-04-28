using UnityEngine;

public class Actor : MonoBehaviour
{
    public GameObject itemToDrop;
    protected int currentHealth;
    public int maxHealth;
    public float itemLaunchForce = 5f;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        // print("Taking Damage: " + amount);
        currentHealth -= amount;
        // print("Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Death();
        }
        
    }

    public void Death()
    {
        GameObject droppedItem = Instantiate(itemToDrop, transform.position, Quaternion.identity);

        Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomDir = (Random.insideUnitSphere + Vector3.up).normalized;
            rb.AddForce(randomDir * itemLaunchForce, ForceMode.Impulse);
            
            Vector3 randomTorque = new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(-20f, 20f),
                Random.Range(-10f, 10f)
            );
            rb.AddTorque(randomTorque, ForceMode.Impulse);
        }
        GameManager.instance.enemiesLiving--;
        StatsManager.instance.money++;
        GameManager.instance.UpdateUI();
        Destroy(gameObject);
    }
}
