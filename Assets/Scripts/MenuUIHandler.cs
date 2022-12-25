using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor; // Conditional import. This is a strictly development package.
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    /// <summary>
    /// Handle logic when a new color is selected.
    /// Currently saves to singleton state manager.
    /// </summary>
    /// <param name="color"></param>
    public void NewColorSelected(Color color)
    {
        // Save to singleton state manager.
        MainManager.Instance.teamColor = color;
    }

    /// <summary>
    /// Starts a new game.
    /// </summary>
    /// <remarks>
    /// Game screen is expected to be build order 1. Defined in File >> Build Settings.
    /// </remarks>
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Exit the application. If within Unity Editor, this function exits playmode.
    /// </summary>
    /// <remarks>
    /// Conditional compiling is used. Depending on the flags set during compilation, only 1 of the 2 lines are kept.
    /// </remarks>
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;
    }
}
