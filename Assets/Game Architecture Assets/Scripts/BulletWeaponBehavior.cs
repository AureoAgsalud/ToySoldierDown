using UnityEngine;

public class BulletWeaponBehavior : IWeaponBehavior
{
    ShootInteractor shootInteractor;
    Transform shootPoint;

    private float bulletDamage = 10f;
    private float bulletSpeedMultiplier = 1.5f;
    public AudioSource audioSource;
    public BulletWeaponBehavior(ShootInteractor _interactor)
    {
        shootInteractor = _interactor;
        shootPoint = _interactor.GetShootPoint();
        shootInteractor.GetWeaponRenderer().material.color = Color.green;
        audioSource = _interactor.GetAudioSource();
    }

    public void FireWeapon()
    {
        PooledObject pooledBullet = ObjectPool.Instance.GetPooledObject("Bullet");
        if (pooledBullet != null)
        {
            pooledBullet.gameObject.SetActive(true);
            
            // - Get pooled objects projectile script and initialize it
            ProjectileScript projectileScript = pooledBullet.GetComponent<ProjectileScript>();
            projectileScript.Initialize(shootInteractor.gameObject.tag, bulletDamage);

            // - Get the Rigidbody and set the position and rotation of the bullet
            Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = shootPoint.rotation;

            // - Apply a force to the bullet
            bullet.linearVelocity = shootPoint.forward * shootInteractor.GetShootVelocity();
            
            if(audioSource != null && shootInteractor.bulletShootSound != null)
            {
                audioSource.PlayOneShot(shootInteractor.bulletShootSound);
            }
            // - Recycle the bullet with the object pool
            pooledBullet.DestroyWithTime(2f);
        }
    }
}