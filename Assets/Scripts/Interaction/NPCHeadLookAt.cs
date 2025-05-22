using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Animations.Rigging;

public class NPCHeadLookAt : MonoBehaviour
{

    [SerializeField] private Rig rig;
    [SerializeField] private Transform headLookAtTransform;

    bool isLookingAtPosition = false;

    void Start()
    {

    }

    void Update()
    {
        float targetWeight = isLookingAtPosition ? 1.0f : 0.0f;
        float lerpSpeed = 2f;
        rig.weight = Mathf.Lerp(rig.weight, targetWeight, Time.deltaTime * lerpSpeed);
    }

    public void LookAtPosition(Vector3 lookAtPosition)
    {
        isLookingAtPosition = true;
        headLookAtTransform.position = lookAtPosition;
        Debug.Log("lookAtPosition" + lookAtPosition);
    }
}
