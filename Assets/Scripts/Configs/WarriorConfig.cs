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

    /** 기본 방어력 */
    public static float DEFENSE_POINT = 0f;
    /** 방어구 방어력 */
    public static float AMORY_DEFENSE_POINT = 1f;
    /** 완전 방어 확률 */
    public static float PROB_PERFECT_DEFENSE = 0.2f;

    /** 크리티컬 발생 시, 기본 데미지에 곱할 값 */
    public static float CRITICAL_RATIO = 1.5f;
    /** 크리티컬 발생 확률 */
    public static float PROB_CRITICAL = 0.5f;
    /** 근거리 공격 명중률 */
    public static float PROB_ACCURACY_MELEE = 0.9f;
    /** 원거리 공격 명중률 */
    public static float PROB_ACCURACY_RANGED = 0.3f;

    /** 근거리 공격 속도 : 1초에 1번 공격*/
    public static float MELEE_ATTACK_SPEED = 1.0f;
    /** 원거리 공격 속도 1초에 0.3번 공격*/
    public static float RANGED_ATTACK_SPEED = 0.3f;

    /** 타겟이 Miss, Perfect defense 했을 때 추가되는 공격 대기 시간 */
    public static float DISADVANTAGE_ATTACK_MILLISECONDS = 1000f;
}
