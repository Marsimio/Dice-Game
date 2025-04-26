using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsPlayer;
    
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    public int explosionDamage;
    public int explosionRange;
    private bool hasExploded = false;

    
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch;
    
    int collisions;
    PhysicsMaterial physicMaterial;

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        if (collisions >= maxCollisions) Explode();
        
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet") || collision.collider.CompareTag("Enemy")) return;
        
        collisions++;
        
    }
    
    private void Setup()
    {
        physicMaterial = new PhysicsMaterial();
        physicMaterial.bounciness = bounciness;
        physicMaterial.frictionCombine = PhysicsMaterialCombine.Minimum;
        physicMaterial.bounceCombine = PhysicsMaterialCombine.Maximum;
        GetComponent<SphereCollider>().material = physicMaterial;
        
        rb.useGravity = useGravity;
    }

    private void Explode()
    {
        if (hasExploded) return; 
        hasExploded = true;
        
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
        
        Collider[] targets = Physics.OverlapSphere(transform.position, explosionRange, whatIsPlayer);
        foreach (Collider target in targets)
        {
            PlayerActions player = target.GetComponent<PlayerActions>();
            if (player != null)
            {
                print(explosionDamage);
                player.TakeDamage(explosionDamage);
            }
        }
        
        Invoke("Delay", 0.05f);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
