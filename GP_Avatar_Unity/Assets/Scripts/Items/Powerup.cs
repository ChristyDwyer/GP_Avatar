using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private MeshRenderer _itemMeshRenderer;
    private Collider _itemCollider;
    
    public GameObject pickupEffect;
    public PowerupType powerupType;
    
    public static float SpeedBonus = 1.5f;
    public static float PowerupDuration = 8f;

    private void Awake()
    {
        if (_itemMeshRenderer == null || _itemCollider == null)
        {
            _itemMeshRenderer = GetComponent<MeshRenderer>();
            _itemCollider = GetComponent<Collider>();
        }

        _itemMeshRenderer.enabled = true;
        _itemCollider.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        Transform myTransform = transform;
        //GameObject.Instantiate(pickupEffect, myTransform.position, myTransform.rotation);
        
        _itemMeshRenderer.enabled = false;
        _itemCollider.enabled = false;
        
        switch (powerupType)
        {
            case PowerupType.DoubleJump:
            {
                break;
            }
                
            case PowerupType.SpeedBoost:
            {
                player.GetComponent<PlayerManager>().HandleSpeedBoost(true, SpeedBonus);
                yield return new WaitForSeconds(PowerupDuration);
                player.GetComponent<PlayerManager>().HandleSpeedBoost(false, 1f);
                
                break;
            }
        }
    }
}

public enum PowerupType {DoubleJump, SpeedBoost}
