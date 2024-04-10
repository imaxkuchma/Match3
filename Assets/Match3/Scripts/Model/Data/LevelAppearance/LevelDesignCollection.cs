using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    [CreateAssetMenu(fileName = "LevelDesignCollection", menuName = "Match3/LevelDesignCollection")]
    public class LevelDesignCollection : ScriptableObject
    {
        private static LevelDesignCollection _instance;
        public static LevelDesignCollection Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<LevelDesignCollection>("Match3/LevelDesigns/LevelDesignCollection");
                }
                return _instance;
            }
        }

        public List<LevelDesignAssets> levelDesigns;

        public LevelDesignAssets GetlevelDesignByType(LevelDesignType type)
        {
            var levelDesign = levelDesigns.Find(e => e.type == type);
            return levelDesign;
        }
    }
}

