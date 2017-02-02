using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fattleheart.battle
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField]
        private WarriorStatusBar _statusBar;
        public float HealthPoint
        {
            get
            {
                return _statusBar.HealthPoint;
            }
        }


        private float _myAttackPower;
        private float _myWeaponAttackPower;

        private float _myDefensePoint;
        private float _myAmoryDefensePoint;
        private float _myPerfectDefenseProbability;

        private float _myCriticalRatio;
        private float _myCriticalProbability;
        private float _myHitAccuracy;

        private float _myAttackSpeed;
        private float _millisecPerMeleeAttack;

        void Start()
        {
            _statusBar.init(WarriorConfig.MAX_HP / 2f, WarriorConfig.MAX_ENERGY / 2f);

            // status init
            _myAttackPower = WarriorConfig.ATTACK_POWER_MELEE / 2f;
            _myWeaponAttackPower = WarriorConfig.MELEE_WEAPON_ATTACK_POWER / 2f;

            _myDefensePoint = WarriorConfig.DEFENSE_POINT / 2f;
            _myAmoryDefensePoint = WarriorConfig.AMORY_DEFENSE_POINT / 2f;
            _myPerfectDefenseProbability = WarriorConfig.PROB_PERFECT_DEFENSE / 2f;

            _myCriticalRatio = WarriorConfig.CRITICAL_RATIO / 2f;
            _myCriticalProbability = WarriorConfig.PROB_CRITICAL / 2f;
            _myHitAccuracy = WarriorConfig.PROB_ACCURACY_MELEE / 2f;

            _myAttackSpeed = WarriorConfig.MELEE_ATTACK_SPEED;
            _millisecPerMeleeAttack = BattleUtility.GetMillisecondsPerAttack(_myAttackSpeed);
        }

        void Update()
        {

        }

        public void Die()
        {
            gameObject.SetActive(false);
        }


        public float AttackTo(PlayerView inTarget)
        {
            return 0f;
        }

        public float TakeDamage(float inDamage)
        {
            if (inDamage <= 0f)
            {
                // Play Evade effect
                Debug.Log(string.Format("[EnemyView] Attacked! but evade"));
                return 0f;
            }

            float myDamage = inDamage - (WarriorConfig.DEFENSE_POINT + WarriorConfig.AMORY_DEFENSE_POINT);

            if (myDamage <= 0f)
            {
                // Play perfect guard effect
                Debug.Log(string.Format("[EnemyView] Attacked! but perfect defense"));
                return myDamage;
            }

            // set HP to status Bar
            _statusBar.HealthPoint -= myDamage;

            Debug.Log(string.Format("[EnemyView] Attacked! inDamage = {0} | remainHealth = {1}", myDamage, _statusBar.HealthPoint));

            // play hit effect
            if (_statusBar.HealthPoint <= 0)
            {
                Die();
            }

            return myDamage;
        }
    }

}
