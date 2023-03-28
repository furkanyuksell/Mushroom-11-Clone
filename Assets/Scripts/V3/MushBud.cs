using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushBud : MonoBehaviour
{
    public Vector2 budPos;
    public bool register;
    public MushroomManager MushroomManager;
    public Mushroom mushroom;

    public void DestroyedBud()
    {
        mushroom.FloodFillNeighbor(budPos);
        //mushroom.ChangeRegister();
    }

    public void ItsDeadNow()
    {
        mushroom.RemoveFromActives(budPos);
    }

}
