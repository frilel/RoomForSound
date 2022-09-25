//This is a modified version of https://gist.github.com/eppz/1ebbc1cf6a77741f56d63d3803e57ba3
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

public class BuildPostProcessor
{
//     [PostProcessBuildAttribute(1)]
//     public static void OnPostProcessBuild(BuildTarget target, string path)
//     {
//     }

//     static void AddFrameworks(PBXProject project, string targetGUID)
//     {
//         project.AddFrameworkToProject(targetGUID, "Speech.framework", false);
//         //This project appears to be a default now:
//         //project.AddFrameworkToProject(targetGUID, "AVFoundation.framework", false);
//         // Add `-ObjC` to "Other Linker Flags".
//         project.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-ObjC");
//     }
}