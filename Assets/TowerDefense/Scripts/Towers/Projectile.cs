using System;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Towers
{
    public class Projectile : MonoBehaviour, IPoolable<IMemoryPool>
    {
        private float _moveSpeed;
        
        public Action<Projectile> OnCompleteMove;
        public IGunTarget Target { get; private set; }

        public void MoveToTarget(Vector3 startPosition, IGunTarget target, float moveSpeed = 10.0f)
        {
            Target = target;
            _moveSpeed = moveSpeed;
            transform.position = startPosition;
        }

        public void CustomUpdate()
        {
            if (Target is { IsCanByTarget: true })
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    Target.Transform.position, _moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, Target.Transform.position) <= 0.1f)
                {
                    OnCompleteMove?.Invoke(this);
                }
            }
        }

        #region Spawn

        private IMemoryPool _memoryPool;
        void IPoolable<IMemoryPool>.OnDespawned()
        {
            Target = null;
            OnCompleteMove = null;
        }

        void IPoolable<IMemoryPool>.OnSpawned(IMemoryPool memoryPool)
        {
            _memoryPool = memoryPool;
        }
        
        public void ReturnToPool()
        {
            _memoryPool.Despawn(this);
        }
        
        public class Factory : PlaceholderFactory<Projectile>
        {
        }

        #endregion
    }
}