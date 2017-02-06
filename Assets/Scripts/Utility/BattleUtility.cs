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

        public static byte[] ConvertFromVector3ToByteArray(Vector3 inVector)
        {
            byte[] buff = new byte[sizeof(float) * 3];

            System.Buffer.BlockCopy(System.BitConverter.GetBytes(inVector.x), 0, buff, 0 * sizeof(float), sizeof(float));
            System.Buffer.BlockCopy(System.BitConverter.GetBytes(inVector.y), 0, buff, 1 * sizeof(float), sizeof(float));
            System.Buffer.BlockCopy(System.BitConverter.GetBytes(inVector.z), 0, buff, 2 * sizeof(float), sizeof(float));

            return buff;
        }

        public static Vector3 ConvertFromByteArrayToVector3(byte[] inBytes)
        {
            Vector3 vect = Vector3.zero;

            vect.x = System.BitConverter.ToSingle(inBytes, 0 * sizeof(float));
            vect.y = System.BitConverter.ToSingle(inBytes, 1 * sizeof(float));
            vect.z = System.BitConverter.ToSingle(inBytes, 2 * sizeof(float));

            return vect;
        }
    }

}
