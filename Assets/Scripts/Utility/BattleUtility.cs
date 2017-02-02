using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fattleheart.battle
{
    public class BattleUtility
    {
        public static float GetMillisecondsPerAttack(float inAttackSpeed)
        {
            float result = 1000f / inAttackSpeed;
            return result;
        }
    }

}
