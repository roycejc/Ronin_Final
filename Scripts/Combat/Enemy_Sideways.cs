using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float distanceMoved;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private bool moveingLeft;
    private float leftSide;
    private float rightSide;

    private void Awake()
    {
        leftSide = transform.position.x - distanceMoved;
        rightSide = transform.position.x + distanceMoved;
    }

    private void Update()
    {
        if(moveingLeft)
        {
            if (transform.position.x > leftSide)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                moveingLeft = false;
        }
        else
        {
            if (transform.position.x < rightSide)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                moveingLeft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().Damaged(damage);
        }
    }



}