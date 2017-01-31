using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorConfig
{
    public static float MAX_ENERGY = 100f;
    public static float MAX_HP = 100f;

    /** 기본 공격력 */
    public static float ATTACK_POWER_MELEE = 1f;
    /** 무기 공격력 */
    public static float MELEE_WEAPON_ATTACK_POWER = 2f;
    /** 원거리 무기 공격력 */
    public static float RANGED_WEAPON_ATTACK_POWER = 3f;

    /** 크리티컬 발생 시, 기본 데미지에 곱할 값 */
    public static float CRITICAL_RATIO = 1.5f;
    /** 크리티컬 발생 확률 */
    public static float PROB_CRITICAL = 0.3f;
    /** 근거리 공격 명중률 */
    public static float PROB_ACCURACY_MELEE = 0.8f;
    /** 원거리 공격 명중률 */
    public static float PROB_ACCURACY_RANGED = 0.3f;

    /** 근거리 공격 속도 */
    public static float MELEE_ATTACK_SPEED = 1f;
    /** 원거리 공격 속도 */
    public static float RANGED_ATTACK_SPEED = 5f;
}
