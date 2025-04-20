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
    public Button tryAgainButton;

    public int healthyHeartRateMin = 60;
    public int healthyHeartRateMax = 100;

    private float totalHeartRate = 0f;
    private int sampleCount = 0;
    private bool resultEvaluated = false;

    private SwitchToXRRig rigSwitcher;

    void Start()
    {
        resultPanel?.SetActive(false);
        nextLevelButton?.gameObject.SetActive(false);
        exitButton?.gameObject.SetActive(false);
        previousLevelButton?.gameObject.SetActive(false);
        tryAgainButton?.gameObject.SetActive(false);

        // Cache XR rig switcher reference
        rigSwitcher = FindObjectOfType<SwitchToXRRig>();
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

            // 🔹 Switch to XR rig for UI interaction
            rigSwitcher?.SwitchRig();

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

        // 🔹 Always show exit button
        exitButton?.gameObject.SetActive(true);

        if (averageHR >= healthyHeartRateMin && averageHR <= healthyHeartRateMax)
        {
            resultMessage.text = $"✅ Success! Avg HR: {roundedHR} BPM\nYou can proceed!";
            nextLevelButton?.gameObject.SetActive(true);

            previousLevelButton?.gameObject.SetActive(false);
            tryAgainButton?.gameObject.SetActive(false);
        }
        else
        {
            resultMessage.text = $"❌ Try Again! Avg HR: {roundedHR} BPM\nStay in the healthy zone.";

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
        tryAgainButton?.gameObject.SetActive(false);
    }
}
