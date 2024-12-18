using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float _speed = 10f;

    public void Fly()
    {
        transform.parent = null;
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }
}
