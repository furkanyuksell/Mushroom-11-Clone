using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private ChildPosition playerPrefab;
    [SerializeField] GameObject playerManagerDeactive;
    [SerializeField] GameObject playerGridSystem;
    public int xSize, ySize;
    Dictionary<Tuple<int, int>, ChildPosition> activeDic = new Dictionary<Tuple<int, int>, ChildPosition>();

    private void Start() {
        CreatePlayer();
    }

    public ChildPosition Neighbor(int PosX, int PosY)
    {
        if(activeDic.TryGetValue(new Tuple<int, int>(PosX,PosY), out ChildPosition child))
            return child;
        return null;
    }

    private void CreatePlayer()
    {
        var instantiatedParent = Instantiate(playerGridSystem, this.transform.position, Quaternion.identity);
        float spriteSize= 1f;
        Vector2 startPos = instantiatedParent.transform.position;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                ChildPosition tile = Instantiate(playerPrefab);
                tile.transform.position = new Vector2(startPos.x +  spriteSize * x, startPos.y + spriteSize * y);
                tile.transform.parent = instantiatedParent.transform;
                tile.posX = (int)Math.Round(tile.transform.localPosition.x);
                tile.posY = (int)Math.Round(tile.transform.localPosition.y);
                tile.register = false;
                tile.PlayerManager = this;
                activeDic.Add(new Tuple<int, int>(tile.posX, tile.posY), tile);
            }
        }
    }

    private void Update() {
        if (Input.GetMouseButton(0))
        {
            Collider2D collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition), LayerMask.GetMask("HitTrigger"));
            
            if (collider != null)
            {
                var temp = collider.transform.parent.GetComponent<ChildPosition>();
                activeDic.Remove(new Tuple<int, int>(temp.posX,temp.posY));
                temp.NeighborControl();
                collider.transform.parent.transform.parent = playerManagerDeactive.transform;
                collider.transform.parent.gameObject.SetActive(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            foreach(var item in activeDic)
            {
                Debug.Log(item);
            }
        }
    }
}
