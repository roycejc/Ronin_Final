using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float baseHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float invulnDuration;
    [SerializeField] private int flashCount;
    private SpriteRenderer sprtRendr;
    [SerializeField] private AudioClip hurtSnd;

    private void Awake()
    {
        currentHealth = baseHealth;
        anim = GetComponent<Animator>();
        sprtRendr = GetComponent<SpriteRenderer>();
    }

    public void Damaged(float minusHearts)
    {
        currentHealth = Mathf.Clamp(currentHealth - minusHearts, 0, baseHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invuln());
            SoundManager.instance.PlaySound(hurtSnd);
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }

    public void AddHealth(float heal)
    {
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, baseHealth);
    }

    private IEnumerator Invuln()
    {
        //start iFrame
        Physics2D.IgnoreLayerCollision(10, 11, true);
        //iFrame duration
        for (int i = 0; i < flashCount; i++)
        {
            sprtRendr.color = new Color(1, 0, 0, 1f);
            yield return new WaitForSeconds(1);
            sprtRendr.color = Color.white;
            yield return new WaitForSeconds(1);
        }
        //end iFrame
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //    Damaged(1);        
    }
}
