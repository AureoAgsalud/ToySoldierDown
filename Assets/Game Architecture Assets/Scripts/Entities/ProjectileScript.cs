using System.Runtime.CompilerServices;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private string ownershipTag = "";
    private float damage = 10;

    // - Referecen for th rocket explosion
    public GameObject rocketExplosionPrefab;
    public void Initialize(string _ownershipTag, float _damage)
    {
        ownershipTag = _ownershipTag;
        damage = _damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // - Use EnemyTag
        if (!collision.gameObject.CompareTag(ownershipTag))
        {
            HealthScript health = collision.gameObject.GetComponent<HealthScript>();
            if (health != null)
            {
                health.TakeDamage(damage);

                // - Spawn the explosion
                if (rocketExplosionPrefab != null)
                {
                    Instantiate(rocketExplosionPrefab, transform.position, Quaternion.identity);
                    Destroy(rocketExplosionPrefab, 1f);
                }
            }
        }


        // - Remove bullet if it hits something
        PooledObject pooledObject = GetComponent<PooledObject>();
        if (pooledObject != null)
        {
            pooledObject.ResetObject();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //public float projectileSpeed = 5f;
    //public float damage = 10f;

    //private void Update()
    //{
    //    transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    // - Add damage log (health System) here.
    //    HealthScript health = other.GetComponent<HealthScript>();
    //    if (health != null)
    //    {
    //        health.TakeDamage(damage);
    //    }

    //    Destroy(this.gameObject);
}
