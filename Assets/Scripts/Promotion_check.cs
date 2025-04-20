using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Promotion_check : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI heartRateText;
    public TextMeshProUGUI resultMessage;
    public GameObject resultPanel;

    public Button nextLevelButton;
    public Button exitButton;
    public Button previousLevelButton;
    public Button tryAgainButton; // 🔹 NEW: Try Again button

    public int healthyHeartRateMin = 60;
    public int healthyHeartRateMax = 100;

    private float totalHeartRate = 0f;
    private int sampleCount = 0;
    private bool resultEvaluated = false;

    void Start()
    {
        resultPanel?.SetActive(false);
        nextLevelButton?.gameObject.SetActive(false);
        exitButton?.gameObject.SetActive(false);
        previousLevelButton?.gameObject.SetActive(false);
        tryAgainButton?.gameObject.SetActive(false); // 🔹 Hide Try Again at start
    }

    void Update()
    {
        if (float.TryParse(timerText.text, out float remainingTime) && remainingTime > 0f)
        {
            if (int.TryParse(heartRateText.text, out int currentHR))
            {
                totalHeartRate += currentHR;
                sampleCount++;
            }
        }
        else if (!resultEvaluated)
        {
            resultEvaluated = true;
            EvaluateHeartRate();
        }
    }

    void EvaluateHeartRate()
{
    resultPanel?.SetActive(true);

    if (sampleCount == 0)
    {
        resultMessage.text = "No heart rate data collected.";
        return;
    }

    float averageHR = totalHeartRate / sampleCount;
    int roundedHR = Mathf.RoundToInt(averageHR);

    // 🔹 Always show Exit button
    exitButton?.gameObject.SetActive(true);

    if (averageHR >= healthyHeartRateMin && averageHR <= healthyHeartRateMax)
    {
        resultMessage.text = $"✅ Success! Avg HR: {roundedHR} BPM\nYou can proceed!";
        nextLevelButton?.gameObject.SetActive(true);

        // Hide fail buttons only
        previousLevelButton?.gameObject.SetActive(false);
        tryAgainButton?.gameObject.SetActive(false);
    }
    else
    {
        resultMessage.text = $"❌ Try Again! Avg HR: {roundedHR} BPM\nStay in the healthy zone.";

        // Show fail buttons
        nextLevelButton?.gameObject.SetActive(false);
        previousLevelButton?.gameObject.SetActive(true);
        tryAgainButton?.gameObject.SetActive(true);
    }
}

    public void ResetMonitor()
    {
        totalHeartRate = 0f;
        sampleCount = 0;
        resultEvaluated = false;

        resultMessage.text = "";
        resultPanel?.SetActive(false);

        nextLevelButton?.gameObject.SetActive(false);
        exitButton?.gameObject.SetActive(false);
        previousLevelButton?.gameObject.SetActive(false);
        tryAgainButton?.gameObject.SetActive(false); // 🔹
    }
}
