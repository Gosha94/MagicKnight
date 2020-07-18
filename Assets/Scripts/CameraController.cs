using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0F;

    [SerializeField]
    private Transform target;

    // При загрузке объекта со скриптом
    private void Awake()
    {
        if (!target) target = FindObjectOfType<Character>().transform;
    }
    private void Update()
    {
        Vector3 position = target.position;
        position.z = -2.0F;
        // Плавно(с пом Lerp) перемещаем камеру за игроком
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}
