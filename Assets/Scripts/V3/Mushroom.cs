using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Mushroom : MonoBehaviour
{
    Dictionary<Vector2, MushBud> activeDic = new Dictionary<Vector2, MushBud>();

    public void AddBudToMushroom(MushBud bud)
    {
        activeDic.Add(bud.budPos, bud);
    }

    public void FloodFillNeighbor(Vector2 startPos)
    {
        Queue<Vector2> queue = new Queue<Vector2>();
        queue.Enqueue(startPos);

        while (queue.Count > 0)
        {
            Vector2 currentPos = queue.Dequeue();
            if (activeDic.TryGetValue(currentPos, out MushBud bud))
            {
                if (!bud.register)
                {
                    bud.GetComponent<SpriteRenderer>().color = Color.red;
                    bud.register = true;
                    Vector2[] neighbors = new Vector2[]
                    {
                        new Vector2(currentPos.x - 1, currentPos.y),
                        new Vector2(currentPos.x + 1, currentPos.y),
                        new Vector2(currentPos.x, currentPos.y - 1),
                        new Vector2(currentPos.x, currentPos.y + 1)
                    };

                    foreach (Vector2 neighbor in neighbors)
                    {
                        if (activeDic.ContainsKey(neighbor))
                        {
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }
        }
        activeDic.Remove(startPos);
    }

    public void FloodFillNeigh(Vector2 startPos)
    {
        Queue<Tuple<Vector2, int>> neighQueue = new Queue<Tuple<Vector2, int>>();
        neighQueue.Enqueue(new Tuple<Vector2, int>(startPos, 0));

        while(neighQueue.Count > 0)
        {
            Tuple<Vector2, int> tuple = neighQueue.Dequeue();
            Vector2 currentPos = tuple.Item1;
            if (activeDic.TryGetValue(currentPos, out MushBud bud))
            {
                if (!bud.register)
                {
                    bud.register = true;
                    List<Tuple<Vector2, int>> neighbors = new List<Tuple<Vector2, int>>
                    {
                        new Tuple<Vector2, int>(new Vector2(currentPos.x - 1, currentPos.y), 1),
                        new Tuple<Vector2, int>(new Vector2(currentPos.x + 1, currentPos.y), 2),
                        new Tuple<Vector2, int>(new Vector2(currentPos.x, currentPos.y - 1), 3),
                        new Tuple<Vector2, int>(new Vector2(currentPos.x, currentPos.y + 1), 4)
                    };


                    foreach (var neighbor in neighbors)
                    {
                        if (activeDic.ContainsKey(neighbor.Item1))
                        {
                            neighQueue.Enqueue(new Tuple<Vector2, int>(neighbor.Item1, neighbor.Item2));
                        }
                    }
                }
                
            }
        }
    }

    public void ChangeRegister()
    {
        foreach (var item in activeDic)
        {
            item.Value.register = false;
        }
    }
}
