using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI valueText;

    public void UpdateBar(float currentValue, float maxValue)
    {
        fillBar.fillAmount = currentValue / maxValue;
        valueText.text = currentValue.ToString() + "/" + maxValue.ToString();
    }
}
