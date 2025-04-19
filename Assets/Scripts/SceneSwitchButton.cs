using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneSwitchButton : MonoBehaviour
{
    public string sceneName;
    public SceneLoader sceneLoader;

    public void OnButtonSelected(SelectEnterEventArgs args)
    {
        Debug.Log("Button Clicked!"); // <--- Add this
        sceneLoader.LoadScene(sceneName);
    }
}