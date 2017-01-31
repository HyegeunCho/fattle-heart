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


        void Start()
		{
            _statusBar.init(WarriorConfig.MAX_HP, WarriorConfig.MAX_ENERGY);
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
        public int AttackTo(EnemyView inCharacter)
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


