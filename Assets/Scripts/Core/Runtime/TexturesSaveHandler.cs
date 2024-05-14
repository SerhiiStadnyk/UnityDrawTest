using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Core.Runtime
{
    public class TexturesSaveHandler : MonoBehaviour
    {
        private string SaveFileFolderName => "SavedTextures";
        
        private string SaveFilesPath
        {
            get
            {
                string combinedPath = Path.Combine(Application.persistentDataPath, SaveFileFolderName);
                return combinedPath.Replace("/", Path.DirectorySeparatorChar.ToString());
            }
        }


        public void SaveTextureToFile(Texture2D texture, string fileName)
        {
            if (texture == null)
            {
                Debug.LogError("Texture is null.");
                return;
            }

            if (!DoesFolderExist(SaveFilesPath))
            {
                CreateFolder(SaveFilesPath);
            }

            string filePath = Path.Combine(SaveFilesPath, fileName);
            
            byte[] pngBytes = texture.EncodeToPNG();
            
            File.WriteAllBytes(filePath, pngBytes);
        }


        public Texture2D LoadTextureFromFile(string fileName)
        {
            string filePath = Path.Combine(SaveFilesPath, fileName);

            if (!File.Exists(filePath))
            {
                Debug.LogError("File does not exist: " + filePath);
                return null;
            }
            
            byte[] pngBytes = File.ReadAllBytes(filePath);
            
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(pngBytes);

            return texture;
        }


        public void DeleteSaveFile(string fileName)
        {
            string filePath = Path.Combine(SaveFilesPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log("File deleted: " + filePath);
            }
            else
            {
                Debug.LogWarning("File does not exist: " + filePath);
            }
        }


        public string[] GetSaveFilesNames()
        {
            if (!Directory.Exists(SaveFilesPath))
            {
                Debug.LogWarning("Save directory does not exist: " + SaveFilesPath);
                return Array.Empty<string>();
            }

            string[] files = Directory.GetFiles(SaveFilesPath);
            List<string> saveFileNames = new List<string>();

            foreach (string filePath in files)
            {
                string fileName = Path.GetFileName(filePath);
                saveFileNames.Add(fileName);
            }

            return saveFileNames.ToArray();
        }
        
        
        private bool DoesFolderExist(string path)
        {
            string[] pathSegments = path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            
            string currentPath = "";
            foreach (string segment in pathSegments)
            {
                currentPath = Path.Combine(currentPath, segment);
                if (!Directory.Exists(currentPath))
                {
                    return false;
                }
            }
        
            return true;
        }
        
        
        private void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Debug.Log("Folder created: " + path);
            }
            else
            {
                Debug.LogWarning("Folder already exists: " + path);
            }
        }
    }
}
