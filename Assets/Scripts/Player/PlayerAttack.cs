using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Camera cam;
    AudioSource audioSource;
    
    public LayerMask attackLayer;
    
    public GameObject hitEffect;
    public AudioClip hitSound;
    public AudioClip swordSwing;
    
    private bool attacking = false;
    private bool readyToAttack = true;
    private int attackCount;

    public void Attack()
    {
        print("yerp");
        if(!readyToAttack || attacking) return;
        
        readyToAttack = false;
        attacking = true;
        
        Invoke(nameof(ResetAttack), StatsManager.instance.attackSpeed);
        Invoke(nameof(AttackRaycast), StatsManager.instance.attackDelay);
        
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(swordSwing);
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, StatsManager.instance.attackDistance,
                attackLayer))
        {
            HitTarget(hit.point);

            if (hit.transform.TryGetComponent<Actor>(out Actor actor))
            {
                actor.TakeDamage(StatsManager.instance.attackDamage);
            }
        }
    }

    void HitTarget(Vector3 pos)
    {
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(hitSound);

        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20f);
    }
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void DamageRoll()
    {
        
    }
}
