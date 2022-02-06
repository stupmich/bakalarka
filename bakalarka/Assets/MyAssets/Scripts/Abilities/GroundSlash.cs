using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlash : MonoBehaviour
{
    public float speed = 5;
    public float slowDownRate = 0.01f;
    public float detectingDistance = 0.1f;
    public float destroyDelay = 5;

    private Rigidbody rb;
    private bool stopped;

    public event System.Action<Enemy> OnHit;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (GetComponent<Rigidbody>() != null )
        {
            rb = GetComponent<Rigidbody>();
            StartCoroutine(SlowDown());
        } else
        {
            Debug.Log("No rigidbody");
        }

        Destroy(gameObject, destroyDelay);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!stopped)
        {
            RaycastHit hit;
            Vector3 distance = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

            if (Physics.Raycast(distance,transform.TransformDirection(-Vector3.up), out hit, detectingDistance))
            {
                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            } else
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
            Debug.DrawRay(distance, transform.TransformDirection(-Vector3.up * detectingDistance), Color.red);
        }
    }

    IEnumerator SlowDown()
    {
        float t = 1;

        while (t > 0)
        {
            rb.velocity = Vector3.Lerp(Vector3.zero, rb.velocity, t);
            t -= slowDownRate;
            yield return new WaitForSeconds(0.1f);
        }
        stopped = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (OnHit != null)
            {
                OnHit(other.gameObject.GetComponent<Enemy>());
            }
        }
    }
}
