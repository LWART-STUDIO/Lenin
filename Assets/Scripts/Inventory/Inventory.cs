using System;
using UnityEngine;
using Zenject;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Money _money;
        [SerializeField] private Parapharm _parapharm;
        [SerializeField] private Life _life;

        [Inject]
        private void Construct()
        {

        }

        public int GetMoneyCount()
        {
            return _money.GetCount();
        }

        public void GiveMoney(int count)
        {
            _money.Add(count);
        }

        private void Start()
        {
            _parapharm.ParapharmBecomeZerro += RemoveLife;
        }

        public void RemoveLife()
        {
            if(_life.GetCount()==0)
                return;
            _life.Remove(1);
            MakePharapharmFull();
        }

        public int GetLifeCount()
        {
            return _life.GetCount();
        }
        public int GetParapharmCount()
        {
            return _parapharm.GetCount();
        }
        public void SetUpMoney(int count)
        {
            _money.SetUp(count,999999999);
        }

        public void SetUpLife(int count)
        {
            _life.SetUp(count,3);
        }
        public void SetUpParapharm(int count)
        {
            _parapharm.SetUp(count, 100);
        }
        

        [NaughtyAttributes.Button()]
        private void MakePharapharmFull()
        {
            _parapharm.SetUp(100, 100);
        }
    }
}
