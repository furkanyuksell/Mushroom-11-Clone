using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class MushroomManager : MonoBehaviour
{
    [SerializeField] private MushBud mushBud;
    [SerializeField] GameObject deactiveMushroom;
    [SerializeField] Mushroom mushroomBase;
    public int xSize, ySize;
    Dictionary<Tuple<int, int>, MushBud> activeDic = new Dictionary<Tuple<int, int>, MushBud>();


    //actively use
    MushBud tempMushBud;

    private void Start() {
        CreatePlayer();
    }

    public MushBud Neighbor(int PosX, int PosY)
    {
        if(activeDic.TryGetValue(new Tuple<int, int>(PosX,PosY), out MushBud mushBud))
            return mushBud;
        return null;
    }

    private void CreatePlayer()
    {
        var instantiatedParent = Instantiate(mushroomBase, this.transform.position, Quaternion.identity);
        instantiatedParent.MushroomManager = this;
        float spriteSize= 1f;
        Vector2 startPos = instantiatedParent.transform.position;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                MushBud tile = Instantiate(mushBud);
                tile.transform.position = new Vector2(startPos.x +  spriteSize * x, startPos.y + spriteSize * y);
                tile.transform.parent = instantiatedParent.transform;
                tile.posX = (int)Math.Round(tile.transform.localPosition.x);
                tile.posY = (int)Math.Round(tile.transform.localPosition.y);
                tile.register = false;
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
                var temp = collider.transform.parent.GetComponent<MushBud>();
                activeDic.Remove(new Tuple<int, int>(temp.posX,temp.posY));                
                collider.transform.parent.transform.parent = deactiveMushroom.transform;
                collider.transform.parent.gameObject.SetActive(false);
            }
        }

    }
}
