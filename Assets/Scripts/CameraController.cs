using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerController target;
    void Start()
    {
        target = FindObjectOfType<PlayerController>();
        if(target is null)
        {
            Debug.LogError("No target was assigned to the camera");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, 0, -5);
    }
}
