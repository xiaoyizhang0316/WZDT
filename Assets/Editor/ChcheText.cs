using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ChcheText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [MenuItem("Tools/清除存档")]
    private static void Clear()
    {
        DeleteFilesInPath(Application.persistentDataPath);
        PlayerPrefs.DeleteAll();
        EditorUtility.DisplayDialog("注意", "已删除所有存档", "好的");
    }
    //删除这个目录下的所有文件及文件夹
    public static void DeleteFilesInPath(string strPath)
    {
        //删除这个目录下的所有子目录
        if (Directory.GetDirectories(strPath).Length > 0)
        {
            foreach (string var in Directory.GetDirectories(strPath))
            {
                //DeleteDirectory(var);
                Directory.Delete(var, true);
                //DeleteDirectory(var);
            }
        }

        //删除这个目录下的所有文件
        if (Directory.GetFiles(strPath).Length > 0)
        {
            foreach (string var in Directory.GetFiles(strPath))
            {
                File.Delete(var);
            }
        }
    } 
    /// <summary>
    /// 打开文件夹相关的实用函数。
    /// </summary>
    public static class OpenFolder
    {
        /// <summary>
        /// 打开 Data Path 文件夹。
        /// </summary>
        [MenuItem("Tools/Open Folder/Data Path", false, 10)]
        public static void OpenFolderDataPath()
        {
            Execute(Application.dataPath);
        }

        /// <summary>
        /// 打开 Persistent Data Path 文件夹。
        /// </summary>
        [MenuItem("Tools/Open Folder/Persistent Data Path", false, 11)]
        public static void OpenFolderPersistentDataPath()
        {
            Execute(Application.persistentDataPath);
        }

        /// <summary>
        /// 打开 Streaming Assets Path 文件夹。
        /// </summary>
        [MenuItem("Tools/Open Folder/Streaming Assets Path", false, 12)]
        public static void OpenFolderStreamingAssetsPath()
        {
            Execute(Application.streamingAssetsPath);
        }

        /// <summary>
        /// 打开 Temporary Cache Path 文件夹。
        /// </summary>
        [MenuItem("Tools/Open Folder/Temporary Cache Path", false, 13)]
        public static void OpenFolderTemporaryCachePath()
        {
            Execute(Application.temporaryCachePath);
        }

        /// <summary>
        /// 打开指定路径的文件夹。
        /// </summary>
        /// <param name="folder">要打开的文件夹的路径。</param>
        public static void Execute(string folder)
        {
            folder = string.Format("\"{0}\"", folder);
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    Process.Start("Explorer.exe", folder.Replace('/', '\\'));
                    break;

                case RuntimePlatform.OSXEditor:
                    Process.Start("open", folder);
                    break;

                default:
                    throw new Exception(string.Format("Not support open folder on '{0}' platform.", Application.platform.ToString()));
            }
        }
    }
}
