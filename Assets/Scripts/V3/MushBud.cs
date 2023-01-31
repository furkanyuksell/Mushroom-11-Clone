using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushBud : MonoBehaviour
{
    public Vector2 budPos;
    public bool register;
    Dictionary<MushBud, int> neighList = new Dictionary<MushBud, int>();
    public MushroomManager MushroomManager;
    MushBud tempMushBud;
    bool isTriggered = true;


    /*private void Update()
    {
        if ((tempMushBud = MushroomManager.DeactiveNeighbor(posX - 1, posY)) && isTriggered)
        {
            Debug.Log(posX + "," + posY+"|Budın sol tarafı yok oldu");
            isTriggered = false;
        }

        if ((tempMushBud = MushroomManager.DeactiveNeighbor(posX + 1, posY)) && isTriggered)
        {
            Debug.Log(posX + "," + posY + "|Budın sağ tarafı yok oldu");
            isTriggered = false;

        }

        if ((tempMushBud = MushroomManager.DeactiveNeighbor(posX, posY - 1)) && isTriggered)
        {
            Debug.Log(posX + "," + posY + "|Budın ust tarafı yok oldu");
            isTriggered = false;

        }

        if ((tempMushBud = MushroomManager.DeactiveNeighbor(posX, posY + 1)) && isTriggered)
        {
            Debug.Log(posX + "," + posY + "|Budın alt tarafı yok oldu");
            isTriggered = false;

        }
    }*/

    void NeighControl()
    {
        
    }

    private void OnEnable()
    {
        Destroyer.OnNeighborTrigger += NeighControl;
    }

    private void OnDisable()
    {
        Destroyer.OnNeighborTrigger -= NeighControl;   
    }


    public void TriggerMushBud()
    {

    }

}
