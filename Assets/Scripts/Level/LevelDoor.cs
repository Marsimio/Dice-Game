using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (GameManager.instance.enemiesLiving <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
