using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    Vector3 mouseWorldPos;
    [SerializeField] Camera mainCam;
    [SerializeField] MushroomManager mushroomManager;
    MushBud _activeMushBud;
    private void LateUpdate()
    {
        mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        transform.position = mouseWorldPos;
    }

    float cooldown = 0.2f;
    bool hasNewKil = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MushBud>(out MushBud mushBud))
        {
            hasNewKil = true;
            cooldown = 0.2f;
            _activeMushBud = mushBud;

            mushBud.register = true;
            mushroomManager.deactiveDic.Add(mushBud.budPos, mushBud);
            mushBud.transform.parent = mushroomManager.deactiveMushroom.transform;
            mushBud.transform.gameObject.SetActive(false);
        }

    }
    

    private void Update()
    {
        if(hasNewKil)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                _activeMushBud.DestroyedBud();
                hasNewKil = false;
                cooldown = .2f;
            }
        }
    }

}
