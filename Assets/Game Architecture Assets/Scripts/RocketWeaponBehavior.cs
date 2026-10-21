using UnityEngine;
using UnityEngine.Audio;

public class RocketWeaponBehavior : IWeaponBehavior
{
    ShootInteractor shootInteractor;
    Transform shootPoint;
    private float rocketDamage = 25f;
    private float rocketSpeedMultiplier = 0.5f;
    public AudioSource audioSource;
    public RocketWeaponBehavior(ShootInteractor _interactor)
    {
        shootInteractor = _interactor;
        shootPoint = _interactor.GetShootPoint();
        shootInteractor.GetWeaponRenderer().material.color = Color.purple;
        audioSource = _interactor.GetAudioSource();
    }
    public void FireWeapon()
    {
        PooledObject pooledRocket = ObjectPool.Instance.GetPooledObject("Rocket");
        if (pooledRocket != null)
        {
            pooledRocket.gameObject.SetActive(true);

            // - Get pooled objects projectile script and initialize it
            ProjectileScript projectileScript = pooledRocket.GetComponent<ProjectileScript>();
            projectileScript.Initialize(shootInteractor.gameObject.tag, rocketDamage);

            // - Get the Rigidbody and set the position and rotation of the bullet
            Rigidbody rocket = pooledRocket.GetComponent<Rigidbody>();
            rocket.transform.position = shootPoint.position;
            rocket.transform.rotation = shootPoint.rotation;

            // - Apply a force to the bullet
            float finalSpeed = shootInteractor.GetShootVelocity() * rocketSpeedMultiplier;
            rocket.linearVelocity = shootPoint.forward * finalSpeed;

            if (audioSource != null && shootInteractor.bulletShootSound != null)
            {
                audioSource.PlayOneShot(shootInteractor.bulletShootSound);
            }
            // - Recycle the bullet with the object pool
            pooledRocket.DestroyWithTime(2f);
        }
    }
}
