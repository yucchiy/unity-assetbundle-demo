using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

namespace U3d {
  class EditorRun {
    [MenuItem ("U3d/Example/Build")]
    static void Build() {
      Debug.Log("Building Example3");

      BuildTarget Target;
#if UNITY_2017_3_OR_NEWER
      Target = BuildTarget.StandaloneOSX;
#else
      Target = BuildTarget.StandaloneOSXIntel64;
#endif      

      BuildPlayer(EditorBuildSettings.scenes, "target/Example2.app", Target, BuildOptions.None);
    }

    private static void BuildPlayer(EditorBuildSettingsScene[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options) {
      Debug.Log("Target Directory = " + target_dir);
      FileSystemUtil.EnsureParentExists(target_dir);
      // string res = BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options);
      var result = BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options);
      foreach (var step in result.steps)
      {
        foreach (var message in step.messages)
        {
          Debug.Log("Step = " + step.name + ", Type = " + message.type + ", Content = " + message.content);
        }
      }
      // if (res.Length > 0) {
      //  throw new Exception("BuildPlayer failure: " + res);
      // }      
    }
  }

  class FileSystemUtil {
    public static void EnsureParentExists(string target_dir) {
      DirectoryInfo parent = Directory.GetParent(target_dir);
      if (!parent.Exists) {
        Directory.CreateDirectory(parent.FullName);
      }
    }    
  }
}

