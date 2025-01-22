using UnityEngine;

public class Barrel : MonoBehaviour
{
    public float Health = 100;
    public bool HasExploded = false;
    public GameObject DestroyedBarrel;
    public float Radius = 5;
    public float Force = 4000;
    public float MinForce = 2500;
    public float MaxForce = 4000;
    public ParticleSystem TheExplosionParticle;
    public float ExplosionDamage = 10;
    public Transform TheExplotionPointTransform;
    public AudioSource ExplosionSound;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HasExploded)
        {
            return;
        }

        if (Health <= 0 && !HasExploded)
        {
            Explode();
        }

    }


    public void Explode()
    {
        HasExploded = true;
        ExplosionSound.Play();
        DestroyedBarrel.gameObject.SetActive(true);
        MeshRenderer barrelMesh = GetComponent<MeshRenderer>();
        BoxCollider barrelBoxCollider = GetComponent<BoxCollider>();

        barrelMesh.enabled = false;
        barrelBoxCollider.enabled = false;
        Destroy(gameObject, 10);

        TheExplosionParticle.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, Radius);

        foreach (Collider nearbyobject in colliders) 
        {
            Rigidbody rb = nearbyobject.GetComponent<Rigidbody>();
            
            if (rb != null) 
            {
                Force = Random.Range(MinForce, MaxForce);
                rb.AddExplosionForce(Force, TheExplotionPointTransform.position, Radius);
            }

            Enemy anEnemy = nearbyobject.GetComponentInParent<Enemy>();
            if (anEnemy != null)
            {
                anEnemy.Health -= (Random.Range(30, 70) * Time.deltaTime * ExplosionDamage);
                if (!anEnemy.HasExploded)
                {
                    anEnemy.HitReact();
                    anEnemy.HasExploded = true;
                    StartCoroutine(anEnemy.HasExplodedTimer());
                }
                
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(TheExplotionPointTransform.position, Radius);
       
    }
}
