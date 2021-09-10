using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetPlayer;
    public float smoothing;

    void Start()
    {

    }

    void LateUpdate()
    {
        if(targetPlayer != null)
        {
            if(transform.position != targetPlayer.position)
            {
                Vector3 targetPos = targetPlayer.position;
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);

            }
        }
    }
    void Update()
    {

    }


}
