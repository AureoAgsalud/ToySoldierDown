using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
public class EnemyAttackState : EnemyState
{
    private bool isCooldownActive = false;
    
    public EnemyAttackState(EnemyController enemyController) : base(enemyController)
    {
        _controller = enemyController;
    }

    public override void OnStateEntered()
    {
        Debug.Log("Enemy has entered Attack State");
        
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy has exited Attack State");
        
    }

    public override void OnStateUpdate()
    {
        Debug.Log("Attack... Update");

        // -Read distance to player
        float distance = Vector3.Distance(_controller.transform.position, _controller.target.transform.position);
        Vector3 lookAtTarget = _controller.target.transform.position;
        lookAtTarget.y = _controller.transform.position.y;
        _controller.gameObject.transform.LookAt(lookAtTarget);

        // - Add any combat/attack logic here
        // - Deal damage to a players health bar
        // - use a resource (mana, bullet, etc);
        // - Shoot a projectile at the player


        //Kiting Mechanic
        float comfortZoneThreshold = _controller.attackRange * 0.8f;

        if (distance < comfortZoneThreshold)
        {
            Vector3 retreatDirection = (_controller.transform.position - _controller.target.transform.position).normalized;
            Vector3 targetRetreatPosition = _controller.transform.position + retreatDirection;

            targetRetreatPosition.y = _controller.transform.position.y;

            _controller.gameObject.transform.position = Vector3.MoveTowards(_controller.transform.position,
                targetRetreatPosition, _controller.moveSpeed * Time.deltaTime);
            // - Go back to follow state if player is out of attack range
            if (distance > _controller.attackRange)
            {
                _controller.ChangeState(new EnemyFollowState(_controller));
            }

            //if (_controller.canAttack)
            if (_controller.canAttack && !isCooldownActive)
            {
                ExecuteEnemyAttack();
            }
            void ExecuteEnemyAttack()
            {

                PooledObject pooledBullet = ObjectPool.Instance.GetPooledObject("EnemyBullet");

                if (pooledBullet != null)
                {
                    pooledBullet.gameObject.SetActive(true);

                    // - Get pooled objects projectile script and initialize it
                    ProjectileScript projectileScript = pooledBullet.GetComponent<ProjectileScript>();
                    projectileScript.Initialize(_controller.gameObject.tag, _controller.GetAttackDamage());

                    // - Get the Rigidbody and set the position and rotation of the bullet
                    Rigidbody projectile = pooledBullet.GetComponent<Rigidbody>();
                    projectile.transform.position = _controller.projectileSpawnReference.transform.position;

                    Vector3 floorTarget = _controller.target.transform.position;
                    floorTarget.y = _controller.transform.position.y;

                    Vector3 targetDirection = (floorTarget - _controller.projectileSpawnReference.transform.position).normalized;

                    projectile.transform.rotation = Quaternion.LookRotation(targetDirection);

                    //projectile.transform.rotation = _controller.transform.rotation;

                    // - Apply a force to the bullet
                    projectile.linearVelocity = _controller.transform.forward * _controller.GetProjectileSpeed();

                    // - Recycle the bullet with the object pool
                    pooledBullet.DestroyWithTime(2f);
                }

                _controller.StartCoroutine(AttackDelay());
            }


        }
    }
    public IEnumerator AttackDelay()
    {
        isCooldownActive = true;
        _controller.canAttack = false;
        yield return new WaitForSeconds(_controller.GetAttackCooldown());
        _controller.canAttack = true;
        isCooldownActive = false;
    }

}