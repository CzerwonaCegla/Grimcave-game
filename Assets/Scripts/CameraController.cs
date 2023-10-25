using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    Vector3 toPos;

    void Update()
    {
        toPos = new Vector3(Player.transform.position.x, 0, -10);
        transform.position = toPos;
    }
}
