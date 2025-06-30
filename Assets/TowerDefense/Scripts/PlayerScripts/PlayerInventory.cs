using System.Collections.Generic;
using UniRx;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerInventory
    {
        private Dictionary<string , int> _itemsdict = new();
        public IReadOnlyDictionary<string , int> Items => _itemsdict;
        public ReactiveCommand<string> OnChangeCollection = new();
        
        public void AddItem(string item)
        {
            if (!_itemsdict.TryAdd(item, 1))
            {
                _itemsdict[item] += 1;
            }

            OnChangeCollection?.Execute(item);
        }
        
        public void RemoveItem(string item)
        {
            if (_itemsdict.ContainsKey(item))
            {
                _itemsdict[item] -= 1;
            }
            
            OnChangeCollection?.Execute(item);
        }

        public int GetCountItem(string item)
        {
            return _itemsdict.GetValueOrDefault(item, 0);
        }
        
        public void ClearCollection()
        {
            _itemsdict.Clear();
            OnChangeCollection?.Execute("");
        }
    }
}