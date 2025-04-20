using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int healAmount;
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name + " is walked in");
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerActions>().HealDamage(healAmount); 
            gameObject.SetActive(false);
        }
        
    }
}
