using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Towers
{
    public class Projectile : MonoBehaviour, IPoolable<IMemoryPool>
    {
        public Action<Projectile> OnCompleteMove;
        public IGunTarget Target { get; set; }
        
        private float _moveSpeed;
        
        private Vector3 _targetPosition;
        private readonly CompositeDisposable _disposable = new();

        public void MoveToTarget(Vector3 startPosition, IGunTarget target, float moveSpeed = 10.0f)
        {
            Target = target;
            _moveSpeed = moveSpeed;
            transform.position = startPosition;
            _targetPosition = target.Transform.position;

            _disposable.Clear();
            Target.OnDead.Subscribe(unit =>
            {
                _disposable?.Clear();
                Target = null;
            }).AddTo(_disposable);
        }

        public void CustomUpdate()
        {
            if (Target is { IsCanByTarget: true })
            {
                _targetPosition = Target.Transform.position;
            }
            else
            {
                Target = null;
            }
            
            transform.position = Vector3.MoveTowards(transform.position,
                _targetPosition, _moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _targetPosition) <= 0.1f)
            {
                OnCompleteMove?.Invoke(this);
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

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}