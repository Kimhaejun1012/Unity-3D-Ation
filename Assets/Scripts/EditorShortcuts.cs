using UnityEditor;

public class EditorShortcuts
{
    [MenuItem("Custom/Controls/PlayPause _F1")]
    private static void PlayPauseToggle()
    {
        EditorApplication.isPlaying = !EditorApplication.isPlaying;
    }

    [MenuItem("Custom/Controls/Pause _F2")]
    private static void Pause()
    {
        EditorApplication.isPaused = !EditorApplication.isPaused;
    }
}
