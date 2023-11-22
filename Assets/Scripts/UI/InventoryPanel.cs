using System;
using System.Collections.Generic;
using TMPro;
using UIHealthAlchemy;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private TMP_Text _parapharmText;
        [SerializeField] private MaterialHealhBar _materialHealhBar;
        [SerializeField] private Sprite _fullHurt;
        [SerializeField] private Sprite _emptyHurt;
        [SerializeField] private List<Image> _hurts;
        private int _lifeCount;
        private Inventory.Inventory _inventory;

        public void SetUp(Inventory.Inventory inventory)
        {
            _inventory = inventory;
        }

        private void Update()
        {
            if(_inventory==null)
                return;
            _moneyText.text = $"Money: {_inventory.GetMoneyCount()}";
            _parapharmText.text = $"{_inventory.GetParapharmCount()}";
            _materialHealhBar.Value = Mathf.Lerp(_materialHealhBar.Value,_inventory.GetParapharmCount() / 100f,2f*Time.deltaTime);
            _lifeCount = _inventory.GetLifeCount();
            for (int i = 0; i < _hurts.Count; i++)
            {
                if(_lifeCount>i)
                    _hurts[i].sprite= _fullHurt;
                else
                {
                    _hurts[i].sprite= _emptyHurt;
                }
                
            }

        }
    }
}
