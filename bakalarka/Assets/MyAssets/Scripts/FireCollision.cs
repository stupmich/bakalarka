using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollision : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem ps;
    [SerializeField]
    public GameObject player;

    public GameObject playerFirePrefab;
    GameObject childObject = null;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        player = GameObject.Find("Player");
    }

    void OnParticleTrigger()
    {
        if (childObject == null)
        {
            childObject = Instantiate(playerFirePrefab, player.transform.position + new Vector3(0,2.2f,-0.05f), Quaternion.identity) as GameObject;
            childObject.transform.parent = player.transform;
        }
    }
}
