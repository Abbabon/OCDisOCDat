using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

/// <summary>
/// Automatically increments Android versionCode before each build.
/// Uses Android nomenclature: versionCode is the monotonically increasing integer,
/// versionName is the human-readable string (e.g. "0.5").
///
/// versionCode (AndroidBundleVersionCode) increments by 1 every build.
/// versionName (bundleVersion) uses a simple minor bump scheme:
///   0.4 -> 0.5 -> 0.6 ... -> 0.9 -> 0.10 -> 0.11 ...
///
/// The script compares against the current values and only bumps if a build
/// is actually happening (Unity calls this via IPreprocessBuildWithReport).
/// </summary>
public class BuildVersionIncrementer : IPreprocessBuildWithReport
{
    // Lower number = runs earlier. We want this to run before anything else.
    public int callbackOrder => 0;

    // Baseline: version 0.4 shipped as versionCode 4.
    private const int BaseVersionCode = 4;
    private const string BaseVersionName = "0.4";

    public void OnPreprocessBuild(BuildReport report)
    {
        int currentVersionCode = PlayerSettings.Android.bundleVersionCode;
        string currentVersionName = PlayerSettings.bundleVersion;

        int newVersionCode = currentVersionCode + 1;
        string newVersionName = BumpMinorVersion(currentVersionName);

        Debug.Log($"[BuildVersionIncrementer] Build detected for {report.summary.platform}");
        Debug.Log($"[BuildVersionIncrementer] versionCode: {currentVersionCode} -> {newVersionCode}");
        Debug.Log($"[BuildVersionIncrementer] versionName: {currentVersionName} -> {newVersionName}");

        PlayerSettings.Android.bundleVersionCode = newVersionCode;
        PlayerSettings.bundleVersion = newVersionName;

        // Persist the changes so they survive if Unity doesn't auto-save.
        AssetDatabase.SaveAssets();
    }

    /// <summary>
    /// Bumps the minor component of a version string: "0.4" -> "0.5", "1.9" -> "1.10".
    /// Falls back to appending ".1" if the format is unexpected.
    /// </summary>
    private static string BumpMinorVersion(string version)
    {
        if (string.IsNullOrEmpty(version))
            return "0.5";

        int lastDot = version.LastIndexOf('.');
        if (lastDot < 0)
        {
            // No dot found, treat whole string as major, append .1
            return version + ".1";
        }

        string majorPart = version.Substring(0, lastDot);
        string minorStr = version.Substring(lastDot + 1);

        if (int.TryParse(minorStr, out int minor))
        {
            return majorPart + "." + (minor + 1);
        }

        // Couldn't parse minor, just bump from 1
        return majorPart + ".1";
    }
}
