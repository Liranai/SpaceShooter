using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Vector3.up * Time.deltaTime);

        if (transform.position.y >= 7.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
