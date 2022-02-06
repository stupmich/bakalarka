using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    public Vector3 offset;
    public float pitch = 2f;
    private float currentZoom = 10f;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    public GameObject player;

    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        player = GameObject.Find("Player");
        target = player.transform;
    }

    void Update()
    {
        //zooming
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        //check zoom value between min and max
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    void LateUpdate()
    {
        //position of camera
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
    }
}
