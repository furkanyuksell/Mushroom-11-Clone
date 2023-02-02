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
        var temp = collision.transform.GetComponent<MushBud>();
        temp.DestroyedBud();
        mushroomManager.deactiveDic.Add(temp.budPos, temp);
        temp.register = false;
        collision.transform.parent = mushroomManager.deactiveMushroom.transform;
        collision.transform.gameObject.SetActive(false);

    }
}
