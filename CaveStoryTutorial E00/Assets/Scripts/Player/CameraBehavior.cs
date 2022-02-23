using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform target;
    public float dampingTime = 1f;
    public float PPU = 16f;

    private Vector3 velocity;

    private Vector3 proxyPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        proxyPosition = Vector3.SmoothDamp(proxyPosition, target.position, ref velocity, dampingTime);

        transform.position = new Vector3(Mathf.Round(proxyPosition.x * PPU) / PPU, Mathf.Round(proxyPosition.y * PPU) / PPU, -10f);

        transform.position += Vector3.forward * -10f;
    }
}
