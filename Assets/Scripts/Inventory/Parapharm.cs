using System;
using UnityEngine;

namespace Inventory
{
    public class Parapharm : InventoryItem
    {
        private int _amount = 1;
        private float _time = 0.1f;
        private float _startTime =  0.1f;
        public Action ParapharmBecomeZerro;
        private void Update()
        {
            if (_time <= 0)
            {
                Remove(_amount);
                _time = _startTime;
            }
            else
            {
                _time -= Time.deltaTime;
            }

            if (GetCount() <= 0)
            {
                ParapharmBecomeZerro?.Invoke();
            }
        }
    }
}
