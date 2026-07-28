using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class StartScreen : MonoBehaviour
{
    [SerializeField] string sceneName;

    void Start()
    {
        if (string.IsNullOrEmpty(sceneName))
            Debug.LogError($"{nameof(sceneName)} parameter is empty");
    }

    private void OnEnable()
    {
        InputSystem.onAnyButtonPress.CallOnce(OnAnyButtonPressed);
    }

    void OnAnyButtonPressed(InputControl control)
    {
        SceneManager.LoadScene(sceneName);
    }
}
