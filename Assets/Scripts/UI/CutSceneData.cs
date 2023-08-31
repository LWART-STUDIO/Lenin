using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "CutScene Data",menuName = "Add/Create CutScene Data")]
    public class CutSceneData : ScriptableObject
    {
        public string Name;
        public List<Sprite> CutSceneSprites;
    }
}