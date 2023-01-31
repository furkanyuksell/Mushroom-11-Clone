using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushBud : Mushroom
{
    public int posX;
    public int posY;
    public bool register;
    Dictionary<MushBud, int> neighList = new Dictionary<MushBud, int>();
    MushBud tempMushBud;

    private void Update()
    {
        if (tempMushBud = MushroomManager.Neighbor(posX - 1, posY))
        {

        }

        if (tempMushBud = MushroomManager.Neighbor(posX + 1, posY))
        {

        }

        if (tempMushBud = MushroomManager.Neighbor(posX, posY - 1))
        {

        }

        if (tempMushBud = MushroomManager.Neighbor(posX, posY + 1))
        {

        }
    }

    public void TriggerMushBud()
    {

    }

}
