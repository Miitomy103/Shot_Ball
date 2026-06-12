using UnityEngine;

[ExecuteAlways]
public class SliderUI : MonoBehaviour
{
    [Header("Sliderの値を変えるやつ")]
    [SerializeField, Range(0, 1)] float sliderValue = 0;

    float maxPos;
    float minPos;
    RectTransform thisRect;
    [Header("いじらなくていいやつ")]
    [SerializeField] RectTransform sliderRect;
    [SerializeField] RectTransform backGround;
    [SerializeField] RectTransform line;
    [SerializeField] RectTransform childLine;

    [SerializeField] Vector2 backRatio;
    [SerializeField] Vector2 lineRatio;
    [SerializeField] Vector2 childLineRatio;


    Vector2 lastRectSize = Vector2.zero;

    private void Awake()
    {
        thisRect = GetComponent<RectTransform>();
        UpdateMinMax();
    }

    private void Start()
    {
        SetSliderValue(sliderValue);
        ApplyLayout();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (thisRect == null) thisRect = GetComponent<RectTransform>();

        // サイズが変わったときだけ処理
        if (thisRect.rect.size != lastRectSize)
        {
            ApplyLayout();
            lastRectSize = thisRect.rect.size;
        }
#endif
    }

    private void OnValidate()
    {
        Debug.Log("OnValidate called, updating slider value: " + sliderValue);

        // thisRect が null の場合は取得する
        if (thisRect == null)
            thisRect = GetComponent<RectTransform>();

        UpdateMinMax();

        if (sliderRect != null)
        {
            SetSliderValue(sliderValue);
        }

        ApplyLayout();
    }

    void UpdateMinMax()
    {
        if (backGround != null)
        {
            maxPos = backGround.rect.width;
            minPos = 0;
        }
    }

    public void SetSliderValue(float value)
    {
        float clampedValue = value * maxPos;
        if (sliderRect != null)
            sliderRect.anchoredPosition = new Vector2(clampedValue, sliderRect.anchoredPosition.y);
    }

    void ApplyLayout()
    {
        if (backGround != null)
            backGround.sizeDelta = thisRect.rect.size * (backRatio / 100);

        if (line != null)
            line.sizeDelta = thisRect.rect.size * (lineRatio / 100);

        if (childLine != null)
            childLine.sizeDelta = thisRect.rect.size * (childLineRatio / 100);

        UpdateMinMax();
    }
}
