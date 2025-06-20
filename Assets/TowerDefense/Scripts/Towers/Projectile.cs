using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Towers
{
    public class Projectile : MonoBehaviour
    {
        public Action<Projectile> OnCompleteMove;
        public ITarget Target { get; set; }
        
        private float _moveSpeed;
        private Vector3 _targetPosition;
        private readonly CompositeDisposable _disposable = new();

        public void MoveToTarget(Vector3 startPosition, ITarget target, float moveSpeed = 10.0f)
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
        
        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}