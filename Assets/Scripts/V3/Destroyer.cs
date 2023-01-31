using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    Vector3 mouseWorldPos;
    [SerializeField] Camera mainCam;
    [SerializeField] MushroomManager mushroomManager;
    private void LateUpdate()
    {
        mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        transform.position = mouseWorldPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var temp = collision.transform.parent.GetComponent<MushBud>();
        Debug.Log(temp.posX + " - " + temp.posY );
        mushroomManager.deactiveDic.Add(new Tuple<int, int>(temp.posX, temp.posY), temp);
        collision.transform.parent.transform.parent = mushroomManager.deactiveMushroom.transform;
        collision.transform.parent.gameObject.SetActive(false);
    }
}
