using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMoveCommand : BattleCommand
{
    private List<Vector3> _waypoints;
    public List<Vector3> WayPoints
    {
        get
        {
            return _waypoints;
        }
    }

    public void AddWaypoint(Vector3 inPosition)
    {
        if (_waypoints == null)
        {
            _waypoints = new List<Vector3>();
        }
        
        if (_waypoints.Contains(inPosition))
        {
            return;
        }
        _waypoints.Add(inPosition);
    }



    public BattleMoveCommand(List<Vector3> inWaypoints = null)
    {
        type = ECommandType.MOVE;

        if (inWaypoints == null)
        {
            _waypoints = new List<Vector3>();
        }
        else
        {
            _waypoints = inWaypoints;
        }

        // type + waypointCount + {waypoints}
        byteSize = sizeof(int) + sizeof(int) + (sizeof(float) * 3 * _waypoints.Count);
    }

    public byte[] Serialize()
    {
        byte[] buff = new byte[byteSize];

        int destOffset = 0;
        System.Buffer.BlockCopy(System.BitConverter.GetBytes((int)type), 0, buff, destOffset, sizeof(int));
        destOffset += sizeof(int);

        System.Buffer.BlockCopy(System.BitConverter.GetBytes((int)type), 0, buff, destOffset, sizeof(int));
        destOffset += sizeof(int);

        System.Buffer.BlockCopy(System.BitConverter.GetBytes(_waypoints.Count), 0, buff, destOffset, sizeof(int));
        destOffset += sizeof(int);

        for (int index = 0; index < _waypoints.Count; index++)
        {
            System.Buffer.BlockCopy(System.BitConverter.GetBytes(_waypoints[index].x), 0, buff, destOffset, sizeof(float));
            destOffset += sizeof(float);
            System.Buffer.BlockCopy(System.BitConverter.GetBytes(_waypoints[index].y), 0, buff, destOffset, sizeof(float));
            destOffset += sizeof(float);
            System.Buffer.BlockCopy(System.BitConverter.GetBytes(_waypoints[index].z), 0, buff, destOffset, sizeof(float));
            destOffset += sizeof(float);
        }
        return buff;
    }

    public static BattleMoveCommand Deserialize(byte[] inBytes)
    {
        BattleMoveCommand result = new BattleMoveCommand();

        int destOffset = 0;
        
        result.type = (ECommandType)System.BitConverter.ToInt32(inBytes, destOffset);
        destOffset += sizeof(int);

        int waypointCount = System.BitConverter.ToInt32(inBytes, destOffset);
        destOffset += sizeof(int);
        
        for (int index = 0; index < waypointCount; index++)
        {
            Vector3 position = new Vector3();

            position.x = System.BitConverter.ToSingle(inBytes, destOffset);
            destOffset += sizeof(float);
            position.y = System.BitConverter.ToSingle(inBytes, destOffset);
            destOffset += sizeof(float);
            position.z = System.BitConverter.ToSingle(inBytes, destOffset);
            destOffset += sizeof(float);

            result.AddWaypoint(position);
        }

        return result;
    }
}
