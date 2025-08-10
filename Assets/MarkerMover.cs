using UnityEngine;

public class MarkerMover : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;           // نصف سرعت قبلی
    [SerializeField] private float totalDistance = 1f;   // 30 سانتی‌متر (0.3 واحد)

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float halfDistance = totalDistance / 1.5f;
        float offset = Mathf.PingPong(Time.time * speed, totalDistance) - halfDistance;
        transform.position = startPos + new Vector3(0f, 0f, offset);
    }
}
