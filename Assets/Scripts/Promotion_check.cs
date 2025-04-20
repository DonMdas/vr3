using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Promotion_check : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI heartRateText;
    public TextMeshProUGUI resultMessage;
    public GameObject resultPanel;

    [Header("Buttons")]
    public Button nextLevelButton;
    public Button exitButton;
    public Button previousLevelButton;
    public Button tryAgainButton;

    [Header("Heart Rate Range")]
    public int healthyHeartRateMin = 60;
    public int healthyHeartRateMax = 100;

    [Header("Camera Rigs")]
    public GameObject ovrRig;
    public GameObject xrRig;

    private float totalHeartRate = 0f;
    private int sampleCount = 0;
    private bool resultEvaluated = false;

    void Start()
    {
        // Ensure proper rig activation
        if (ovrRig != null) ovrRig.SetActive(true);
        if (xrRig != null) xrRig.SetActive(false);

        resultPanel?.SetActive(false);
        nextLevelButton?.gameObject.SetActive(false);
        exitButton?.gameObject.SetActive(false);
        previousLevelButton?.gameObject.SetActive(false);
        tryAgainButton?.gameObject.SetActive(false);
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

        // ✅ Always show Exit
        exitButton?.gameObject.SetActive(true);

        string currentSceneName = SceneManager.GetActiveScene().name;
        bool isFirstLevel = currentSceneName == "Level 1";
        bool isLastLevel = currentSceneName == "Level 5";

        previousLevelButton?.gameObject.SetActive(!isFirstLevel);
        nextLevelButton?.gameObject.SetActive(!isLastLevel);

        if (averageHR >= healthyHeartRateMin && averageHR <= healthyHeartRateMax)
        {
            resultMessage.text = $"✅ Success! Avg HR: {roundedHR} BPM\nYou can proceed!";
            nextLevelButton?.gameObject.SetActive(!isLastLevel);
            previousLevelButton?.gameObject.SetActive(false);
            tryAgainButton?.gameObject.SetActive(false);
        }
        else
        {
            resultMessage.text = $"❌ Try Again! Avg HR: {roundedHR} BPM\nStay in the healthy zone.";
            nextLevelButton?.gameObject.SetActive(false);
            previousLevelButton?.gameObject.SetActive(!isFirstLevel);
            tryAgainButton?.gameObject.SetActive(true);
        }

        // ✅ Sync XR Rig position to OVR Rig
        if (ovrRig != null && xrRig != null)
        {
            xrRig.transform.position = ovrRig.transform.position;
            xrRig.transform.rotation = ovrRig.transform.rotation;
            ovrRig.SetActive(false);
            xrRig.SetActive(true);
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

        // Reset camera rigs to original state
        if (ovrRig != null) ovrRig.SetActive(true);
        if (xrRig != null) xrRig.SetActive(false);
    }
}