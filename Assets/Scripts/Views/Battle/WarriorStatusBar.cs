using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fattleheart.battle
{
    public class WarriorStatusBar : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _hpBar;
        [SerializeField]
        private SpriteRenderer _energyBar;

        public float testHealthPoint;
        public float testEnergyPoint;

        private float _totalHealthPoint;
        private float _totalEnergyPoint;

        private float _healthPoint;
        public float HealthPoint
        {
            get
            {
                return _healthPoint;
            }

            set
            {
                if (_hpBar != null)
                {
                    float hpRatio = value / _totalHealthPoint;
                    hpRatio = Mathf.Clamp(hpRatio, 0f, 1f);
                    _hpBar.transform.localScale = new Vector3(hpRatio, 1f, 1f);
                }
                _healthPoint = value;
            }
        }

        private float _energyPoint = 0;
        public float EnergyPoint
        {
            get
            {
                return _energyPoint;
            }

            set
            {
                if (_energyBar != null)
                {
                    float energyRatio = value / _totalEnergyPoint;
                    energyRatio = Mathf.Clamp(energyRatio, 0f, 1f);
                    _energyBar.transform.localScale = new Vector3(energyRatio, 1f, 1f);
                }
                _energyPoint = value;
            }
        }

        public void init(float inMaxHealthPoint, float inMaxEnergyPoint)
        {
            _totalHealthPoint = inMaxHealthPoint;
            _totalEnergyPoint = inMaxEnergyPoint;

            _healthPoint = _totalHealthPoint;
            _energyPoint = 0;
        }
    }
}

