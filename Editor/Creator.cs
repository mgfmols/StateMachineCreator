using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace State_Machine_Creator.Editor
{
    public class Creator : EditorWindow
    {
        private string _selectedPath;
        private string _stateMachineName;
        private string _baseNamespace;
        private string _namespace;
        private bool _invalidDirectory;
        private bool _addNamespace;

        private bool _opened;
                
        private const string PlaceholderTemplate = "###TEMPLATE###";
        private const string PlaceholderNamespace = "###NAMESPACE###";
        
        [MenuItem("Assets/Create/State Machine")]
        public static void StartCreation()
        {
            var window = GetWindow<Creator>("Create new State Machine");
            window.ShowPopup();
        }

        private void OnGUI()
        {
            GUILayout.Label("Create new State Machine", EditorStyles.boldLabel);
            if (!TryGetActiveFolderPath(out _selectedPath))
            {
                Debug.LogError("No active folder can be found. If you believe this is an error, please contact the developer.");
                return;
            }

            if (!_opened)
            {
                var assetPath = _selectedPath;
                if (string.IsNullOrEmpty(assetPath))
                {
                    Debug.LogError("The asset folder cannot be found.");
                    return;
                }
                var firstIndex = assetPath.IndexOf('/');
                var substr = assetPath.Substring(firstIndex + 1);
                _baseNamespace = substr.Replace(" ", "_").Replace("/", ".");
                _namespace = _baseNamespace;
                _opened = true;
            }
            
            var stateMachineName = EditorGUILayout.TextField("State Machine Name", _stateMachineName);
            if (stateMachineName != _stateMachineName)
            {
                _namespace = _baseNamespace + $".{stateMachineName}";
                _stateMachineName = stateMachineName;
            }

            EditorGUILayout.BeginHorizontal();
            _addNamespace = EditorGUILayout.Toggle(_addNamespace, GUILayout.Width(25));
            GUI.enabled = _addNamespace;
            EditorGUILayout.LabelField("Namespace", GUILayout.Width(100));
            _namespace = EditorGUILayout.TextField(_namespace, GUILayout.ExpandWidth(true));
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
            
            _invalidDirectory = DirectoryExists(_selectedPath);
            GUI.enabled = !_invalidDirectory;
            CreateButton(_selectedPath);
            GUI.enabled = true;
            
            ConditionalMessage(_invalidDirectory, true, "A State Machine with this name already exists in this folder.", Color.red);
        }

        private void CreateButton(string path)
        {
            if (!GUILayout.Button("Create"))
            {
                return;
            }
            CreateStateMachine(path);
            Close();
        }

        private static void ConditionalMessage(bool condition, bool inverse, string message, Color color)
        {
            if ((inverse && !condition) || (!inverse && condition))
            {
                return;
            }
            
            var oldColor = GUI.color;
            GUI.color = color;
            EditorGUILayout.LabelField(message);
            GUI.color = oldColor;
        }

        private bool DirectoryExists(string path)
        {
            return Directory.Exists($"{path}/{_stateMachineName}");
        }
        
        // Define this function somewhere in your editor class to make a shortcut to said hidden function
        private static bool TryGetActiveFolderPath(out string path)
        {
            var tryGetActiveFolderPath = typeof(ProjectWindowUtil).GetMethod( "TryGetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic );
            var args = new object[] { null };
            var found = (bool)tryGetActiveFolderPath?.Invoke(null, args)!;
            path = (string)args[0];
            return found;
        }

        private void CreateStateMachine(string path)
        {
            // Create new directory
            path = string.Concat(Application.dataPath.Replace("Assets", ""), path);
            var capitalizedName = CapitalizeFirstLetter(_stateMachineName);
            var directory = $"{path}/{capitalizedName}";
            Directory.CreateDirectory(directory);
            
            var scriptPath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
            if (string.IsNullOrEmpty(scriptPath))
            {
                throw new Exception("Asset directory could not be found.");
            }
            var scriptDirectory = Path.GetDirectoryName(scriptPath);
            var templateDirectory = Path.Combine(scriptDirectory, "Template");
            var templateDictionaryAbsolutePath = Path.GetFullPath(Path.Combine(Application.dataPath, "..", templateDirectory));
            
            foreach (var filePath in Directory.GetFiles(templateDictionaryAbsolutePath))
            {
                if (!File.Exists(filePath))
                {
                    continue;
                }

                var fileName = Path.GetFileName(filePath);
                var newFileName = fileName.Replace("Template", capitalizedName);
                var newDirectory = $"{directory}/{newFileName}";
                    
                File.Copy(filePath, newDirectory, true);
                var content = File.ReadAllText(newDirectory);

                var updatedContent = content
                    .Replace("State_Machine_Creator.Editor.Template", PlaceholderNamespace)
                    .Replace("Template", PlaceholderTemplate);

                updatedContent = updatedContent
                    .Replace(PlaceholderNamespace, _namespace)
                    .Replace(PlaceholderTemplate, capitalizedName);

                // Only write back if there's an actual change
                if (content == updatedContent)
                {
                    continue;
                }
                File.WriteAllText(newDirectory, updatedContent);
            }

            AssetDatabase.Refresh();
        }

        private static string CapitalizeFirstLetter(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text; // Return as-is if empty or null
            }

            return char.ToUpper(text[0]) + text[1..];
        }
    }
}
