using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarUI : MonoBehaviour
{
    public BossHealth bossHealth; // Reference to the BossHealthController script
    public Slider healthSlider; // Reference to the UI Slider

    private void Update()
    {
        if (bossHealth != null && healthSlider != null)
        {
            // Update the slider value based on the boss's current health percentage
            healthSlider.value = bossHealth.GetCurrentHealthPercentage();
        }
    }
}
