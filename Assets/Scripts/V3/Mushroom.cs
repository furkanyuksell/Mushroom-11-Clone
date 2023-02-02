using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    Dictionary<Vector2, MushBud> activeDic = new Dictionary<Vector2, MushBud>();

    public void AddBudToMushroom(MushBud bud)
    {
        activeDic.Add(bud.budPos, bud);
    }

    public void FloodFillNeighbor(Vector2 startPos)
    {
        StartCoroutine(SlowFill(startPos));
    }
    IEnumerator SlowFill(Vector2 startPos)
    {
        Queue<Vector2> queue = new Queue<Vector2>();
        queue.Enqueue(startPos);

        while (queue.Count > 0)
        {
            yield return new WaitForSeconds(0.05f);
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
    public void ChangeRegister()
    {
        foreach (var item in activeDic)
        {
            item.Value.register = false;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
