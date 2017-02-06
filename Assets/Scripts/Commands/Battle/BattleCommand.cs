using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCommand
{
    public enum ECommandType
    {
        NONE = 0,
        UPDATE = 1, 
        MOVE = 2, 
        MOVE_WAYPOINT = 3, 
        ATTACK = 4,
    }

    public ECommandType type;
    public int byteSize;

    public BattleCommand()
    {
        type = ECommandType.NONE;
        byteSize = sizeof(int);
    }
}
