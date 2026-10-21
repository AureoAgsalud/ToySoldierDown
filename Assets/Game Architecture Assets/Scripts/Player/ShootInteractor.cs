using UnityEngine;

public class ShootInteractor : Interactor
{
    [SerializeField] private Input _inputType;

    private IWeaponBehavior _currentShootStrategy;

    [Header("Shoot")]
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private float _shootVelocity;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private PlayerMovementBehavior _playMovementBehavior;

    // - Audio
    [SerializeField] public AudioSource audio;
    [SerializeField] public AudioClip bulletShootSound;

    public MeshRenderer _gunRenderer;
    private float _finalShootVelocity;
    private void Start()
    {
        SwitchWeapon(new BulletWeaponBehavior(this));
    }

    public override void Interact()
    {
        //if (_inputType == Input.Primary && PlayerInput.Instance.primaryShootPressed || _inputType == Input.Secondary && PlayerInput.Instance.secondaryShootPressed)
        if (PlayerInput.Instance.primaryShootPressed || PlayerInput.Instance.secondaryShootPressed)
        {
            Shoot();
        }

        if (PlayerInput.Instance.alpha1Pressed)
        {
            SwitchWeapon(new BulletWeaponBehavior(this));
        }
        if (PlayerInput.Instance.alpha2Pressed)
        {
            SwitchWeapon(new RocketWeaponBehavior(this));
        }
    }
    void Shoot()
    {
        _finalShootVelocity = _playMovementBehavior.GetForwardSpeed() + _shootVelocity;

        _currentShootStrategy.FireWeapon();
        //// - Object Pooling
        //PooledObject pooledBullet = ObjectPool.Instance.GetPooledObject();

        //if (pooledBullet != null)
        //{

        //    // - Get the Rigidbody and set the position and rotation of the bullet
        //    Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
        //    bullet.transform.position = _shootPoint.position;
        //    bullet.transform.rotation = _shootPoint.rotation;

        //    // - Apply a force to the bullet
        //    bullet.linearVelocity = _shootPoint.forward * _shootVelocity;

        //    // - Recycle the bullet with the object pool
        //    pooledBullet.DestroyWithTime(2f);
        //}

        //Rigidbody bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
        //bullet.linearVelocity = _shootPoint.forward * _finalShootVelocity;
        //Destroy(bullet.gameObject, 7f); //remove once object pooling is added
    }
    public Transform GetShootPoint()
    {
        return _shootPoint;
    }
    public float GetShootVelocity()
    {
        return _shootVelocity;
    }
    public MeshRenderer GetWeaponRenderer()
    {
        return _gunRenderer;
    }
    public void SwitchWeapon(IWeaponBehavior newWeapon)
    {
        //responsible for determining which weapon used

        _currentShootStrategy = newWeapon;
        Debug.Log("Switched weapon behavior to: " + newWeapon.GetType().Name);
    }
    public AudioSource GetAudioSource()
    { 
        return audio;
    }
}

public enum Input
{
    Primary, Secondary
}