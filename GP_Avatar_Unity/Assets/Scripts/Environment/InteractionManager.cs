using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject>
{
}

public class InteractionManager : MonoBehaviour
{
    public GameObjectEvent changePlayerContextAction;
    public GameObject connectedDoor;
    public GameObject cutsceneCamera;
    public float buttonGlowIntensity = 4f;

    private PlayableDirector _director;
    private bool _playerInRange;
    private Material _myMaterial;
    private Color _closedEmissionColour;
    private Color _openEmissionColour;

    private void Awake()
    {
        _director = GetComponent<PlayableDirector>();
        _myMaterial = GetComponent<MeshRenderer>().material;
        _closedEmissionColour = Color.red * buttonGlowIntensity;
        _openEmissionColour = Color.green * buttonGlowIntensity;
        
        _myMaterial.SetColor("_EmissionColor", _closedEmissionColour);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
            transform.GetChild(0).gameObject.SetActive(true);
            
            changePlayerContextAction.Invoke(gameObject);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
            transform.GetChild(0).gameObject.SetActive(false);
            
            changePlayerContextAction.Invoke(gameObject);
        }
    }

    public void Interact()
    {
        if (_director.state != PlayState.Playing)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            _director.Play();
        }
    }

    public void ChangeButtonColour()
    {
        _myMaterial.SetColor("_EmissionColor", _openEmissionColour);
    }
    
    public void FinishCutscene()
    {
        Debug.Log("Cutscene Finished!");
    }
}
