using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QueueManager : MonoBehaviour
{
    [SerializeField] GameObject playerGridSystem;
    List<Tuple<int,int, bool>> actives;   
    List<Tuple<int,int, bool>> temp;
    List<Tuple<int,int, bool>> empty; 
    Queue<Tuple<int,int, bool>> queue;
    Queue<Tuple<int,int, bool>> tempQueue;
    List<ChildPosition> objectList;
    private void Awake() {
        actives = new List<Tuple<int,int, bool>>();
        objectList = new List<ChildPosition>();
        queue = new Queue<Tuple<int,int, bool>>();
        temp = new List<Tuple<int,int, bool>>();
        empty = new List<Tuple<int,int, bool>>();
        tempQueue = new Queue<Tuple<int,int, bool>>();
    }
    public void SetToItemList(ChildPosition item)
    {
        objectList.Add(item);
    }
    public List<ChildPosition> GetActiveObjectList()
    {
        return objectList;
    }
    public void ReadyToQueue()
    {
        queue.Clear();
        foreach (var item in actives)
        {
            queue.Enqueue(item);
        }
    }
    public void SetToActiveList(int Item1, int Item2, bool Item3)
    {
        actives.Add(new Tuple<int, int, bool>(Item1, Item2, Item3));
    }
    public void RemoveFromActiveList(int Item1, int Item2, bool Item3)
    {
        if(actives.Contains(new Tuple<int, int, bool>(Item1, Item2, Item3)))
        {
            Debug.Log("removed");
            actives.Remove(new Tuple<int, int, bool>(Item1, Item2, Item3));
            FloodFill();
            foreach (var item in actives)
            {
                Debug.Log(item.Item1 + " " + item.Item2 + " " + item.Item3);
            }
        }
    }
    public List<Tuple<int,int, bool>> GetActiveList()
    {
        return actives;
    }
    int i = 0;

    void CopyTuples()
    {
        temp.Clear();
        foreach (var item in actives)
        {
            temp.Add(item);
        }
    }
    void FloodFill()
    {
        int neighborCount = 0;
        i = 0;
        CopyTuples();
        while(temp.Count > 0)
        {
            if(temp[i] != null)
            {
                DynamicFloodFillControl(temp[i]);
                i++;
                neighborCount++;
            }
            if (neighborCount > 1)
            {
                CreateNewGrid();
            }
            empty.Clear();
        }
    }
    void DynamicFloodFillControl(Tuple<int,int, bool> coord)
    {
        int xCoord = coord.Item1;
        int yCoord = coord.Item2;
        bool hasNeighbor = coord.Item3;
        temp.Remove(coord);
        if(!empty.Contains(new Tuple<int, int, bool>(xCoord, yCoord, hasNeighbor)))
            empty.Add(new Tuple<int, int, bool>(xCoord, yCoord, hasNeighbor));
        

        if(temp.Contains(new Tuple<int, int, bool>(xCoord + 1, yCoord, hasNeighbor)))
        {
            empty.Add(new Tuple<int, int, bool>(xCoord + 1, yCoord, hasNeighbor));
            DynamicFloodFillControl(new Tuple<int, int, bool>(xCoord + 1, yCoord, hasNeighbor));
        }
        if(temp.Contains(new Tuple<int, int, bool>(xCoord - 1, yCoord, hasNeighbor)))
        {
            empty.Add(new Tuple<int, int, bool>(xCoord - 1, yCoord, hasNeighbor));
            DynamicFloodFillControl(new Tuple<int, int, bool>(xCoord - 1, yCoord, hasNeighbor));
        }
        if(temp.Contains(new Tuple<int, int, bool>(xCoord, yCoord + 1, hasNeighbor)))
        {
            empty.Add(new Tuple<int, int, bool>(xCoord, yCoord + 1, hasNeighbor));
            DynamicFloodFillControl(new Tuple<int, int, bool>(xCoord, yCoord + 1, hasNeighbor));
        }
        if(temp.Contains(new Tuple<int, int, bool>(xCoord, yCoord - 1, hasNeighbor)))
        {
            empty.Add(new Tuple<int, int, bool>(xCoord, yCoord - 1, hasNeighbor));
            DynamicFloodFillControl(new Tuple<int, int, bool>(xCoord, yCoord - 1, hasNeighbor));
        }
        
        
    }

    private void CreateNewGrid()
    {
        var instantiatedParent = Instantiate(playerGridSystem, this.transform.position, Quaternion.identity);
        foreach (var item in objectList)
        {
            if(empty.Contains(new Tuple<int, int, bool>(item.posX, item.posY, item.register)))
            {
                item.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                item.transform.parent = instantiatedParent.transform;
            }
        }
    }
}
