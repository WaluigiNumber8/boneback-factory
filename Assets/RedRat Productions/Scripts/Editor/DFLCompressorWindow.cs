using System.IO;
using RedRats.Systems.FileSystem.Compression;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Unity.Properties;
using UnityEditor;
using UnityEngine;

namespace RedRat_Productions.Scripts.Editor
{
    /// <summary>
    /// A helper window which can compress/decompress files to DFL/from JSON.
    /// </summary>
    public class DFLCompressorWindow : OdinEditorWindow
    {
        private ICompressionSystem compression;
        private string lastMessage;
        
        [MenuItem( "Tools/RedRat Productions/DFL Compressor")]
        private static void ShowWindow()
        {
            DFLCompressorWindow window = GetWindow<DFLCompressorWindow>();
            window.titleContent = new GUIContent("DFL Compressor");
            window.maxSize = new Vector2(320, 250);
            window.Show();
        }

        [Title("File Path", horizontalLine: false)]
        [SerializeField, TextArea, HideLabel] private string filePath;
        [Button] public void Open() => filePath = EditorUtility.OpenFilePanel("Select your file", "","json,dfl");
        [OnInspectorGUI] private void S1() => GUILayout.Space(48);

        [HorizontalGroup("Buttons"), Button(ButtonSizes.Large), GUIColor(0, 0.75f, 0)] public void Decompress() => DecompressFile();
        [HorizontalGroup("Buttons"), Button(ButtonSizes.Large), GUIColor(0.75f, 0.6f, 0)] public void Compress() => CompressFile();
        [InfoBox("$lastMessage")]
        [OnInspectorGUI] private void S0() => GUILayout.Space(0);

        private void OnBecameVisible() => compression = new DFLCompression();

        private void CompressFile()
        {
            if (string.IsNullOrEmpty(filePath)) throw new InvalidPathException("The path cannot be null");
            if (Path.GetExtension(filePath) != ".json") throw new InvalidPathException("Can only compress JSON files.");
            compression.Compress(filePath);
            lastMessage = $"Compressed file to {Path.GetFileNameWithoutExtension(filePath)}.dfl";
        }

        private void DecompressFile()
        {
            if (string.IsNullOrEmpty(filePath)) throw new InvalidPathException("The path cannot be null");
            string extension = Path.GetExtension(filePath);
            if (extension != ".dfl") throw new InvalidPathException("Can only decompress DFL files.");
            compression.Decompress(filePath, "json");
            lastMessage = $"Decompressed file to {Path.GetFileNameWithoutExtension(filePath)}.json";
        }

    }
}