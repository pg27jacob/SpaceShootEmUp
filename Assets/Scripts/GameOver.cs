using UnityEngine;

public class GameOver : MonoBehaviour
{
    void OnDestroy()
    {
        Debug.Log("Player destroyed! Exiting game...");
        Application.Quit(); // Exits the game in a built application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in the Unity Editor
#endif
    }
}
