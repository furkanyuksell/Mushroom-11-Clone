using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private ChildPosition playerPrefab;
    [SerializeField] QueueManager queueManager;
    [SerializeField] GameObject playerManagerDeactive;
    [SerializeField] GameObject playerGridSystem;
    public int xSize, ySize;
    private void Start() {
        CreatePlayer();
        queueManager.ReadyToQueue();
    }
    private void CreatePlayer()
    {
        var instantiatedParent = Instantiate(playerGridSystem, this.transform.position, Quaternion.identity);
        float spriteSize=1;
        Vector2 startPos = instantiatedParent.transform.position;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                ChildPosition tile = Instantiate(playerPrefab);
                tile.transform.position = new Vector2(startPos.x +  spriteSize * x, startPos.y + spriteSize * y);
                tile.transform.parent = instantiatedParent.transform;
                tile.posX = ((int)tile.transform.localPosition.x);
                tile.posY = ((int)tile.transform.localPosition.y);
                tile.hasNeighbor = true;
                queueManager.SetToItemList(tile);
                queueManager.SetToActiveList(tile.posX,tile.posY, tile.hasNeighbor);
            }
        }
    }

    private void Update() {
        if (Input.GetMouseButton(0))
        {
            Collider2D collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition), LayerMask.GetMask("HitTrigger"));
            
            if (collider != null)
            {
                int x = collider.transform.parent.GetComponent<ChildPosition>().posX;
                int y = collider.transform.parent.GetComponent<ChildPosition>().posY;
                bool z = collider.transform.parent.GetComponent<ChildPosition>().hasNeighbor;
                queueManager.RemoveFromActiveList(x, y, z);
                collider.transform.parent.transform.parent = playerManagerDeactive.transform;
                collider.transform.parent.gameObject.SetActive(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            List<Tuple<int, int, bool>> act = queueManager.GetActiveList();
            foreach (var item in act)
            {
                Debug.Log(item.Item1 + " " + item.Item2 + " " + item.Item3);
            }
        }
    }
}
