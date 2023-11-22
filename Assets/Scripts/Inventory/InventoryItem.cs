using UnityEngine;

namespace Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        private int _count;
        private int _maxCount;

        public void SetUp(int count, int maxCount)
        {
            _count = count;
            _maxCount = maxCount;
        }
        public void Add(int amount)
        {
            _count = Mathf.Min(_count+amount, _maxCount);
        }

        public void Remove(int amount)
        {
            _count = Mathf.Max(_count-amount,0);
        }

        public int GetCount()
        {
            return _count;
        }
    }
}