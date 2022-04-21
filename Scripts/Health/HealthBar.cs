using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image TotalHealthBar;
    [SerializeField] private Image CurrentHealthBar;

    private void Start()
    {
        TotalHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    void Update()
    {
        CurrentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
