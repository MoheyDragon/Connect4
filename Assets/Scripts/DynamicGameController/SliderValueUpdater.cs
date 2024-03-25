using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SliderValueUpdater : MonoBehaviour
{
    [SerializeField] Slider slider;
    TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        slider.onValueChanged.AddListener(UpdateText);
    }
    private void UpdateText(float value)
    {
        text.text = value.ToString();
    }
}
