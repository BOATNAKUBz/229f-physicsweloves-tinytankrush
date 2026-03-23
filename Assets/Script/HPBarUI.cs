using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    public Health playerHealth;
    public Image fillImage;

    void Update()
    {
        if (playerHealth != null)
        {
            fillImage.fillAmount = playerHealth.GetPercent();
        }
    }
}