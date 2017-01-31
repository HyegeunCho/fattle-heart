using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace fattleheart.battle
{
    public class PlayerAttackRange : MonoBehaviour
    {
        private List<EnemyView> _attackTargets;
        public List<EnemyView> AttackTargets
        {
            get
            {
                return _attackTargets;
            }
        }

        void Start()
        {
            clear();
        }

        void OnTriggerEnter2D(Collider2D inCollider)
        {
            Debug.Log(string.Format("[PlayerAttackRange] OnTriggerEnter2D - {0}", inCollider.ToString()));

            EnemyView tempTarget = inCollider.gameObject.GetComponent<EnemyView>();
            if (tempTarget == null || _attackTargets.Contains(tempTarget))
            {
                return;
            }
            _attackTargets.Add(tempTarget);
        }

        void OnTriggerExit2D(Collider2D inCollider)
        {
            Debug.Log(string.Format("[PlaerAttackRange] OnTriggerExit2D - {0}", inCollider.ToString()));

            if (_attackTargets == null || _attackTargets.Count == 0)
            {
                return;
            }

            EnemyView temp = inCollider.gameObject.GetComponent<EnemyView>();
            if (temp != null && _attackTargets.Contains(temp))
            {
                _attackTargets.Remove(temp);
            }
        }

        private void clear()
        {
            if (_attackTargets == null)
            {
                _attackTargets = new List<EnemyView>();
            }
            _attackTargets.Clear();
        }
    }

}
