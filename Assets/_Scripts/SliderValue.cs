using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : Singleton<SliderValue>
{
    [SerializeField] private TextMeshProUGUI minValue;
    [SerializeField] private TextMeshProUGUI maxValue;
    [SerializeField] private Slider slider;
    [SerializeField] private Button button;
    [Space]
    [SerializeField] private RectTransform sliderRect;
    [SerializeField] private RectTransform handleAreaRect;
    [SerializeField] private RectTransform handleRect;
    private int sliderValue = 1;

    private void Start()
    {
        slider.onValueChanged.AddListener(OnChange);
        SetActive(false, false);
        button.onClick.AddListener(ChangeValue);
    }

    private void ChangeValue()
    {
        SetTrueValue(Level.Instance.itemsCount);
    }

    private void OnEnable()
    {
        slider.value = 0;
    }

    private void OnChange(float value)
    {
        sliderValue = Mathf.RoundToInt(value);
        var safe = Map.Instance.currentSafeRange;
        minValue.text = (sliderValue - safe).ToString();
        maxValue.text = (sliderValue + safe).ToString();
    }

    public void SetTrueValue(int value)
    {
        StartCoroutine(SetValueLerp(value));
        SetActive(true, false);
    }

    public void SetActive(bool isSliderActive, bool isButtonActive)
    {
        slider.gameObject.SetActive(isSliderActive);
        if (!isButtonActive)
            slider.interactable = false;
        else
            slider.interactable = true;
        button.gameObject.SetActive(isButtonActive);

        button.gameObject.SetActive(isButtonActive);
        if (isSliderActive && isButtonActive)
            ResetValue();
    }

    private IEnumerator SetValueLerp(int value)
    {
        var answer = Table.Instance.GetAnswerText();
        float currentValue = sliderValue;
        while (true)
        {
            currentValue = Mathf.Lerp(currentValue, value, 0.1f);
            var roundedValue = Mathf.RoundToInt(currentValue);
            answer.text = roundedValue.ToString();
            if (roundedValue == value)
                break;
            yield return null;
        }
        SetActive(false, false);
        WictoryChecker.CheckWin(GetValue());
    }

    public int GetValue()
    {
        return sliderValue;
    }

    public void ResetValue()
    {
        slider.minValue = Level.Instance.itemsRange.x;
        slider.maxValue = Level.Instance.itemsRange.y;
        var safe = Map.Instance.currentSafeRange;

        var sizeHandle = handleRect.sizeDelta;
        sizeHandle.x = safe * 2 / slider.maxValue * sliderRect.sizeDelta.x;
        handleRect.sizeDelta = sizeHandle;

        var sizeHandleArea = handleAreaRect.sizeDelta;
        sizeHandleArea.x = sliderRect.sizeDelta.x - (safe * 2);
        handleAreaRect.sizeDelta = sizeHandleArea;

        slider.value = slider.minValue;
        OnChange(slider.value);
    }
}