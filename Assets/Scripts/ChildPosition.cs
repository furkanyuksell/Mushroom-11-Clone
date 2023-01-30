using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildPosition : MonoBehaviour
{
    public int posX, posY;
    public bool hasNeighbor;
    public PlayerManager PlayerManager;
    [SerializeField] SpriteRenderer sprite;
    public void NeighborControl()
    {
        ChildPosition neigh;

        if (neigh = PlayerManager.Neighbor(posX - 1, posY))
            neigh.ConnectedWithOther();
        if (neigh = PlayerManager.Neighbor(posX + 1, posY))
            neigh.ConnectedWithOther();
        if (neigh = PlayerManager.Neighbor(posX, posY - 1))
            neigh.ConnectedWithOther();
        if (neigh = PlayerManager.Neighbor(posX, posY + 1))
            neigh.ConnectedWithOther();
    }

    void ConnectedWithOther()
    {
        sprite.color = Color.red;

    }

}
