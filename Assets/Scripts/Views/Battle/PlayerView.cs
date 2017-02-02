using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fattleheart.battle
{
	public partial class PlayerView : MonoBehaviour {

        [SerializeField]
        private PlayerAttackRange _attackRangeChecker;

        [SerializeField]
        private WarriorStatusBar _statusBar;

        [SerializeField]
        private PlayerMoveController _moveController;

        private EPlayerActionType _currentAction;

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
            _statusBar.init(WarriorConfig.MAX_HP, WarriorConfig.MAX_ENERGY);

            // status init
            _myAttackPower = WarriorConfig.ATTACK_POWER_MELEE;
            _myWeaponAttackPower = WarriorConfig.MELEE_WEAPON_ATTACK_POWER;

            _myDefensePoint = WarriorConfig.DEFENSE_POINT;
            _myAmoryDefensePoint = WarriorConfig.AMORY_DEFENSE_POINT;
            _myPerfectDefenseProbability = WarriorConfig.PROB_PERFECT_DEFENSE;

            _myCriticalRatio = WarriorConfig.CRITICAL_RATIO;
            _myCriticalProbability = WarriorConfig.PROB_CRITICAL;
            _myHitAccuracy = WarriorConfig.PROB_ACCURACY_MELEE;

            _myAttackSpeed = WarriorConfig.MELEE_ATTACK_SPEED;
            _millisecPerMeleeAttack = BattleUtility.GetMillisecondsPerAttack(WarriorConfig.MELEE_ATTACK_SPEED);
		}

        private float _remainTimeToAttack = 0f;
        void Update()
        {
            // check target in range
            if (_attackRangeChecker.AttackTargets.Count > 0)
            {
                if (_remainTimeToAttack <= 0)
                {
                    foreach (EnemyView target in _attackRangeChecker.AttackTargets)
                    {
                        if (target.gameObject.active == true)
                        {
                            float actualDamage = AttackTo(target);

                            if (actualDamage <= 0f)
                            {
                                // guard or parring - disadvantage to palyer
                                // Play disadvantage effect
                                Debug.DrawLine(gameObject.transform.position, target.gameObject.transform.position, Color.yellow);
                                _remainTimeToAttack += WarriorConfig.DISADVANTAGE_ATTACK_MILLISECONDS;
                            }
                            else
                            {
                                Debug.DrawLine(gameObject.transform.position, target.gameObject.transform.position, Color.red);
                            }
                        }
                        
                    }
                    _remainTimeToAttack = _millisecPerMeleeAttack;
                }
                else
                {
                    _remainTimeToAttack -= (Time.deltaTime * 1000);
                }
            }
        }

        /**
         * param target character
         * return damage to target
         */
        public float AttackTo(EnemyView inCharacter)
        {
            if (inCharacter.HealthPoint <= 0)
            {
                return 0;
            }

            // check accuracy
            float compAccuracy = Random.Range(0, 1f);
            if (compAccuracy > _myHitAccuracy)
            {
                Debug.DrawLine(gameObject.transform.position, inCharacter.gameObject.transform.position, Color.green);
                // Miss!!
                inCharacter.TakeDamage(0f);
                return 0;
            }

            // calculate damage
            float myDamage = _myAttackPower + _myWeaponAttackPower;
            // check critical prob
            float currentCriticalValue = Random.Range(0, 1f);
            if (currentCriticalValue <= _myCriticalProbability)
            {
                myDamage *= _myCriticalRatio;
                StartCoroutine(ShakeCamera());
            }

            // send damage value to target
            float actualDamage = inCharacter.TakeDamage(myDamage);

            // return actual damage
            return actualDamage;
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

        IEnumerator ShakeCamera()
        {
            Vector3 originCameraPosition = Camera.main.transform.position;

            Camera.main.transform.position += new Vector3(10f, 0, 0);

            yield return new WaitForSeconds(0.1f);

            Camera.main.transform.position -= new Vector3(20f, 0, 0);

            yield return new WaitForSeconds(0.1f);

            Camera.main.transform.position = originCameraPosition;

            yield return null;
        }
	}
}


