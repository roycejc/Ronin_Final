using UnityEngine;

public class HealthCanister : MonoBehaviour
{
    [SerializeField] private float healthReturn;
    [SerializeField] private AudioClip healthPU;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(healthPU);
            collision.GetComponent<Health>().AddHealth(healthReturn);
            gameObject.SetActive(false);
        }
    }
}
