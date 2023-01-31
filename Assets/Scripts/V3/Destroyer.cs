using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public static Action OnNeighborTrigger;
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
        if (collision.gameObject.layer == LayerMask.GetMask("HitTrigger"))
        {

        }
        var temp = collision.transform.parent.GetComponent<MushBud>();
        Debug.Log(temp.budPos);
        OnNeighborTrigger?.Invoke();
        mushroomManager.deactiveDic.Add(temp.budPos, temp);
        collision.transform.parent.transform.parent = mushroomManager.deactiveMushroom.transform;
        collision.transform.parent.gameObject.SetActive(false);
    }
}
