using System;

namespace Inventory
{
    public class Life : InventoryItem
    {
        private int _life = 3;

        private void Start()
        {
            SetUp(3,3);
        }
    }
}
