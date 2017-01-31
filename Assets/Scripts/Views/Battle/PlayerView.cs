using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fattleheart.battle
{
	public partial class PlayerView : MonoBehaviour {



        [SerializeField]
        private PlayerAttackRange _attackRangeChecker;

        [SerializeField]
        private GameObject _statusBarContainer;
        [SerializeField]
        private SpriteRenderer _hpBar;
        [SerializeField]
        private SpriteRenderer _energyBar;



		

        private float _totalHealthPoint;
        private float _totalEnergyPoint;

        private float _healthPoint;
        public float HealthPoint
        {
            get
            {
                return _healthPoint;
            }
        }

        private float _energyPoint = 0;
        public float EnergyPoint
        {
            get
            {
                return _energyPoint;
            }
        }

        public void SetEnergyPoint(float inEnergy)
        {
            if (_energyBar != null)
            {
                float energyRatio = _energyPoint / _totalEnergyPoint;
                energyRatio = Mathf.Clamp(energyRatio, 0f, 1f);
                _energyBar.transform.localScale = new Vector3(energyRatio, 1f, 1f);
            }
            _energyPoint = inEnergy;
        }

        public void SetHealthPoint(float inHP)
        {
            if (_hpBar != null)
            {
                float hpRatio = _healthPoint / _totalHealthPoint;
                hpRatio = Mathf.Clamp(hpRatio, 0f, 1f);
                _hpBar.transform.localScale = new Vector3(hpRatio, 1f, 1f);
            }
            _healthPoint = inHP;
        }


        void Start()
		{
            _totalHealthPoint = WarriorConfig.MAX_HP;
            _totalEnergyPoint = WarriorConfig.MAX_ENERGY;

            _healthPoint = _totalEnergyPoint;
            _energyPoint = 0;
		}
        
        void Update()
        {
            if (_attackRangeChecker.AttackTargets.Count > 0)
            {
                Debug.Log("");
            }
        }

        


        /**
         * param target character
         * return damage to target
         */
        public int AttackTo(PlayerView inCharacter)
        {
            return 0;
        }

        /**
         * param dagmage
         * return remain health point
         */
        public int TakeDamage(int inDamage)
        {
            return 0;
        }


        private void UpdateTouch(){}
	}
}


