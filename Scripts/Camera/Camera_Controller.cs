using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField]private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform player;
    [SerializeField] private float inFront;
    [SerializeField] private float camSpeed;
    private float lookAhead;

    private void Update()
    {
        transform.position = new Vector3(player.position.x + lookAhead, player.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (inFront * player.localScale.x), Time.deltaTime * camSpeed);
    }

    //private void MoveToNewRoom(Transform _newRoom)
    //{
    //    currentPosX = _newRoom.position.x;
    //}



}
