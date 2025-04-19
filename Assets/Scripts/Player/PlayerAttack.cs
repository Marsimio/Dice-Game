using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Camera cam;
    AudioSource audioSource;
    
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int atttackDamage = 1;
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
        
        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);
        
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
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance,
                attackLayer))
        {
            HitTarget(hit.point);

            if (hit.transform.TryGetComponent<Actor>(out Actor actor))
            {
                actor.TakeDamage(atttackDamage);
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
