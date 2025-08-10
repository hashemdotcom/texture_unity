using UnityEngine;

public class HandMover : MonoBehaviour
{
    public float speed = 1f; // سرعت حرکت

    void Update()
    {
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W))
            moveZ = 1f;
        else if (Input.GetKey(KeyCode.S))
            moveZ = -1f;

        // حرکت در راستای محور Z
        transform.Translate(0f, 0f, moveZ * speed * Time.deltaTime);
    }
}
