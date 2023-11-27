using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    Vector3 toPos;

    public static CameraController instance;
    public AnimationCurve curve;
    public float ShakeTime = 0.5f;

    void Update()
    {
        toPos = new Vector3(Player.transform.position.x, 0, -10);
        transform.position = toPos;
    }

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator Shake()
    {
        Vector3 Startposition = transform.position;
        float TimeUsed = 0f;

        while (TimeUsed < ShakeTime)
        {
            TimeUsed += Time.deltaTime;
            float strength = curve.Evaluate(TimeUsed / ShakeTime);
            transform.position = Startposition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = Startposition;
    }
}
