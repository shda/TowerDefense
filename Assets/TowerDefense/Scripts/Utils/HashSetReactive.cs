using System.Collections;
using System.Collections.Generic;
using UniRx;

namespace TowerDefense.Scripts.Utils
{
    public class HashSetReactive<T> : IEnumerable<T>
    {
        private readonly HashSet<T> _value = new();
        public ReactiveCommand<T> OnChange = new();

        public IEnumerator<T> GetEnumerator()
        {
            return _value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T val)
        {
            _value.Add(val);
            OnChange?.Execute(val);
        }

        public void Remove(T val)
        {
            _value.Remove(val);
            OnChange?.Execute(val);
        }
    }
}