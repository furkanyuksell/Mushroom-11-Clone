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
        int counter = 0;
        while (queue.Count > 0)
        {
            Vector2 currentPos = queue.Dequeue();
            if (activeDic.TryGetValue(currentPos, out MushBud bud))
            {
                if (!bud.register)
                {   
                    counter++;  
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
                else
                {
                    activeDic.Remove(bud.budPos);
                }
            }
        }
        Debug.Log("counter " + counter + " / activeDic " + activeDic.Count);

        if(activeDic.ContainsKey(startPos))
            activeDic.Remove(startPos);
    }

    public void RemoveFromActives(Vector2 position)
    {
        activeDic.Remove(position);
    }

    public void ChangeRegister()
    {
        foreach (var item in activeDic)
        {
            item.Value.register = false;
        }
    }
}
