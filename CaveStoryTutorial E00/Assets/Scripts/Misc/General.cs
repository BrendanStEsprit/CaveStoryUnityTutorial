using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    NONE
}
public class General
{
    public static Vector2 Direction2Vector(Direction d)
    {
        switch (d)
        {
            case Direction.UP:
                return Vector3.up;
            case Direction.DOWN:
                return Vector3.down;
            case Direction.LEFT:
                return Vector3.left;
            case Direction.RIGHT:
                return Vector3.right;
            case Direction.NONE:
            default:
                return Vector3.zero;
                

        }
    }
} 

    

