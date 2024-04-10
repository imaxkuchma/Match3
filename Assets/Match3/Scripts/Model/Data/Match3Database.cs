using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Match3
{
    [CreateAssetMenu(fileName = "Match3Database", menuName = "Match3/Match3Database")]
    public class Match3Database : ScriptableObject
    {
        [HideInInspector]
        public int currentLevelIndex = -1;
        public List<LevelData> levels = new List<LevelData>();

        //public List<Element> elements = new List<Element>();

        //public List<Piece> pieces = new List<Piece>();

        public LevelData CurrentLevel
        {
            get
            {
                if (currentLevelIndex < 0)
                {
                    currentLevelIndex = 0;
                }
                else if (currentLevelIndex > GetLastLevelIndex())
                {
                    currentLevelIndex = GetLastLevelIndex();
                }

                return levels[currentLevelIndex];
            }
        }

        public int GetLastLevelIndex()
        {
            return levels.Count - 1;
        }

        public void CreateLevel()
        {
            var currentPath = AssetDatabase.GetAssetPath(this);

            currentPath = Path.GetDirectoryName(currentPath);
            if (!Directory.Exists(currentPath + "/Levels"))
            {
                Directory.CreateDirectory(currentPath + "/Levels");
            }

            LevelData levelData = ScriptableObject.CreateInstance<LevelData>();
            levels.Add(levelData);

            var path = currentPath + $"/Levels/LevelData_{levels.Count}.asset";

            AssetDatabase.CreateAsset(levelData, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            currentLevelIndex = GetLastLevelIndex();
        }

        public void DeleteLevel(int levelIndex = -1)
        {
            if (levels.Count == 1) return;

            var currentPath = AssetDatabase.GetAssetPath(this);
            currentPath = Path.GetDirectoryName(currentPath);

            levelIndex = levelIndex != -1 ? levelIndex : currentLevelIndex;

            var assetPath = currentPath + $"/Levels/LevelData_{levelIndex+1}.asset";
            if (File.Exists(assetPath))
            {
                AssetDatabase.DeleteAsset(assetPath);
            }
            levels.RemoveAt(levelIndex);

            for (var i = levelIndex+1; i <= levels.Count; i++)
            {
                AssetDatabase.RenameAsset(currentPath + $"/Levels/LevelData_{i + 1}.asset", $"LevelData_{i}.asset");
            }
            AssetDatabase.SaveAssets();

            if (levels.Count > 0)
            {
                if(levelIndex != 0 && levelIndex > GetLastLevelIndex())
                {
                    currentLevelIndex--;
                }
            }
            else
            {
                currentLevelIndex = -1;
            } 
        }

        public void DeleteLevel(LevelData level)
        {
            if (levels.Contains(level))
            {
                levels.Remove(level);
            }
        }
    }
}



