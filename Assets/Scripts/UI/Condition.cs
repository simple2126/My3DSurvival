using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float startValue;
    public float maxValue;
    public float curValue;
    public float passiveValue;
    public Image imgBar;

    void Awake()
    {
        curValue = startValue;
    }

    void Update()
    {
        imgBar.fillAmount = curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public void Substract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0f);
    }
}
