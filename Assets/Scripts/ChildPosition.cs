using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;

public class ChildPosition : MonoBehaviour
{
    Dictionary<ChildPosition, int> neighList = new Dictionary<ChildPosition, int>();
    public int posX, posY;
    public bool register = false;
    public PlayerManager PlayerManager;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] GameObject playerBase;
    bool isDivided = false;
    bool dofalseWhenNeedNew = true;
    /// <summary>
    ///
    ///  neighList NameKey posVal upleftright   
    ///
    /// 
    /// </summary>
    public void NeighborControl()
    {
        GameObject instantiatedParent;
        neighList.Clear();
        ChildPosition neigh;
        if (neigh = PlayerManager.Neighbor(posX - 1, posY))
        {
            // key left
            if (dofalseWhenNeedNew)
            {
                neigh.RecursiveFloodFill(1, neighList, neigh, null);

            }
            else
            {
                instantiatedParent = Instantiate(playerBase, this.transform.position, Quaternion.identity);
                neigh.RecursiveFloodFill(1, neighList, neigh, instantiatedParent);
                if (instantiatedParent.transform.childCount == 0)
                    Destroy(instantiatedParent);
            }
        }

        if (neigh = PlayerManager.Neighbor(posX + 1, posY))
        {
            //key right
            if (dofalseWhenNeedNew)
            {
                neigh.RecursiveFloodFill(2, neighList, neigh, null);

            }
            else
            {
                instantiatedParent = Instantiate(playerBase, this.transform.position, Quaternion.identity);
                neigh.RecursiveFloodFill(2, neighList, neigh, instantiatedParent);
                if (instantiatedParent.transform.childCount == 0)
                    Destroy(instantiatedParent);
            }
        }

        if (neigh = PlayerManager.Neighbor(posX, posY - 1))
        {
            //key down
            if (dofalseWhenNeedNew)
            {
                neigh.RecursiveFloodFill(3, neighList, neigh, null);
            }
            else
            {
                instantiatedParent = Instantiate(playerBase, this.transform.position, Quaternion.identity);
                neigh.RecursiveFloodFill(3, neighList, neigh, instantiatedParent);
                if (instantiatedParent.transform.childCount == 0)
                    Destroy(instantiatedParent);
            }


        }

        if (neigh = PlayerManager.Neighbor(posX, posY + 1))
        {
            //key up
            if (dofalseWhenNeedNew)
            {
                neigh.RecursiveFloodFill(4, neighList, neigh, null);
            }
            else
            {
                instantiatedParent = Instantiate(playerBase, this.transform.position, Quaternion.identity);
                neigh.RecursiveFloodFill(4, neighList, neigh, instantiatedParent);
                if (instantiatedParent.transform.childCount == 0)
                    Destroy(instantiatedParent);
            }
        }
        dofalseWhenNeedNew = true;
        foreach (KeyValuePair<ChildPosition, int> entry in neighList)
        {
            entry.Key.register = false;
            
        }

    }

    void RecursiveFloodFill(int key, Dictionary<ChildPosition, int> dict, ChildPosition mainNeigh, GameObject playerBase)
    {
        if (!mainNeigh.register)
        {
            dofalseWhenNeedNew = false;
            dict.Add(mainNeigh, key);
            mainNeigh.register = true;
            if (playerBase != null)
            {
                mainNeigh.transform.parent = playerBase.transform;
            }
            ConnectedWithOther(key, mainNeigh);
            ChildPosition neigh;
            if(neigh = PlayerManager.Neighbor(mainNeigh.posX - 1, mainNeigh.posY))
            {
                RecursiveFloodFill(key, dict, neigh, playerBase);
            }

            if (neigh = PlayerManager.Neighbor(mainNeigh.posX + 1, mainNeigh.posY))
            {
                RecursiveFloodFill(key, dict, neigh, playerBase);
            }

            if (neigh = PlayerManager.Neighbor(mainNeigh.posX, mainNeigh.posY - 1))
            {
                RecursiveFloodFill(key, dict, neigh, playerBase);
            }

            if (neigh = PlayerManager.Neighbor(mainNeigh.posX, mainNeigh.posY + 1))
            {
                RecursiveFloodFill(key, dict, neigh, playerBase);
            }
        }


    }


    void ConnectedWithOther(int key, ChildPosition neigh)
    {
        /*
        bool first come

         */
        if (key == 1)
            neigh.sprite.color = Color.red;
        else if (key == 2)
            neigh.sprite.color = Color.black;
        else if (key == 3)
            neigh.sprite.color = Color.blue;
        else if (key == 4)
            neigh.sprite.color = Color.green;
    }

}
