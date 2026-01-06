using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Player_Data
//can add puzzle, scene/position.
{
    public int sceneIndex;

    public Player_Data(int currentSceneIndex)
    {
        sceneIndex = currentSceneIndex;
    }

}


