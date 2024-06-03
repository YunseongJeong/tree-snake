using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : Node
{
    private Vector3 direction = new Vector3();
    private float speed = 5;

    void Start()
    {
        depth = 0;
        nid = 0;
        base.Start();
    }

    void Update()
    {
        MoveByKey();
    }

    private void MoveByKey()
    {
        direction.Set(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            direction.y += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.y -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x += 1;
        }
        faceDirection(direction);
        direction.Normalize();
        
        transform.position += direction * speed * Time.deltaTime;
    }

    private void faceDirection(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        }
    }
}
