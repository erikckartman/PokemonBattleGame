using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavedData
{
    private static SavedData current;

    public static SavedData Current
    {
        get
        {
            if (current == null)
            {
                current = new SavedData();
            }

            return current;
        }
    }

    public int battleCount = 0;
    public Vector2 playerPosition;
}
