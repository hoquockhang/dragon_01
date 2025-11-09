using UnityEditor;

public class BuildScript
{
    public static void BuildIOS()
    {
        string path = "build/ios";
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, path, BuildTarget.iOS, BuildOptions.None);
    }
}
