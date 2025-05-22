using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Animations.Rigging;

// Script is for NPC to look at player or specific objects when they enter desired "look at" range
public class LookAtObjectAnimRig : MonoBehaviour
{

    private Rig rig;
    private float targetWeight;
    private Transform targetTransform;
    private float detectionRange = 5f;

    private void Awake()
    {
        //rig = GetComponent<Rig>();
    }
    private void Start()
    {
        rig = GetComponent<Rig>();
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        targetWeight = 0f;
    }

    private void Update()
    {
        Debug.Log("player " + targetTransform.position);
        Debug.Log("npc " + transform.position);
        float targetDistance = Vector3.Distance(transform.position, targetTransform.position);
        rig.weight = Mathf.Lerp(rig.weight, targetWeight, Time.deltaTime * 3f);
        Debug.Log("Distance: " + targetDistance);
        if (targetDistance < detectionRange)
        {
            targetWeight = 1f;
            Debug.Log("in detection range ");
        }
        else
        {
            targetWeight = 0f;
            Debug.Log("NOT in detection range ");
        }
    }

}
