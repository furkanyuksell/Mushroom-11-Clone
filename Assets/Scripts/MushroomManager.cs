using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class MushroomManager : MonoBehaviour
{
    [SerializeField] private MushBud mushBud;
    [SerializeField] public GameObject deactiveMushroom;
    [SerializeField] Mushroom mushroomBase;
    public int xSize, ySize;

    [SerializeField] GameObject mouseCircle;

    public Dictionary<Vector2, MushBud> activeDic = new Dictionary<Vector2, MushBud>();
    public Dictionary<Vector2, MushBud> deactiveDic = new Dictionary<Vector2, MushBud>();

    //actively use
    MushBud tempMushBud;

    private void Start() {
        CreatePlayer();
    }

    public MushBud ActiveNeighbor(Vector2 vect)
    {
        if(activeDic.TryGetValue(vect, out mushBud))
            return mushBud;
        return null;
    }
    public MushBud DeactiveNeighbor(Vector2 vect)
    {
        if (deactiveDic.TryGetValue(vect, out MushBud mushBud))
            return mushBud;
        return null;
    }

    private void CreatePlayer()
    {
        var instantiatedParent = Instantiate(mushroomBase, this.transform.position, Quaternion.identity);
        float spriteSize= 1f;
        Vector2 startPos = instantiatedParent.transform.position;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                MushBud tile = Instantiate(mushBud);
                tile.MushroomManager = this;
                tile.transform.position = new Vector2(startPos.x +  spriteSize * x, startPos.y + spriteSize * y);
                tile.transform.parent = instantiatedParent.transform;
                tile.budPos = tile.transform.localPosition;
                tile.register = false;
                activeDic.Add(tile.budPos, tile);
            }
        }
    }


    /*
    private void Update() {
        if (Input.GetMouseButton(0))
        {
            Collider2D collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition), LayerMask.GetMask("HitTrigger"));
            
            if (collider != null)
            {
                var temp = collider.transform.parent.GetComponent<MushBud>();
                deactiveDic.Add(new Tuple<int, int>(temp.posX, temp.posY), temp);
                collider.transform.parent.transform.parent = deactiveMushroom.transform;
                collider.transform.parent.gameObject.SetActive(false);
            }
        }
    }*/

    
}
