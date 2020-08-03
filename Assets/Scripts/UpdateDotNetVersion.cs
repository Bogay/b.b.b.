﻿using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class UpdateDotNetVersion : AssetPostprocessor
{
    private static void _OnGeneratedCSProjectFiles()
    {
        Debug.Log("OnGeneratedCSProjectFiles");
        var dir = Directory.GetCurrentDirectory();
        var files = Directory.GetFiles(dir, "*.csproj");
        foreach(var file in files)
            ChangeTargetFrameworkInfProjectFiles(file);
    }

    static void ChangeTargetFrameworkInfProjectFiles(string file)
    {
        var text = File.ReadAllText(file);
        var find = "TargetFrameworkVersion>v4.7.1</TargetFrameworkVersion";
        var replace = "TargetFrameworkVersion>v4.7.2</TargetFrameworkVersion";

        if(text.IndexOf(find) != -1)
        {
            text = Regex.Replace(text, find, replace);
            File.WriteAllText(file, text);
        }
    }

}