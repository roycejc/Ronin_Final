using UnityEngine;

public class CamController : MonoBehaviour
{
    public float moveSpeed;
    [Range(0, 1)]
    public float smoothTime;

    public Transform playerTransform;

    public void FixedUpdate()
    {
        Vector3 pos = GetComponent<Transform>().position;

        pos.x = Mathf.Lerp(pos.x, playerTransform.position.x, smoothTime);
        pos.y = Mathf.Lerp(pos.y, playerTransform.position.y, smoothTime);
        pos.z = transform.position.z;

        GetComponent<Transform>().position = pos;
    }
}