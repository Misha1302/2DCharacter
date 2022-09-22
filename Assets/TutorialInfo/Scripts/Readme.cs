using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TutorialInfo.Scripts
{
    public class Readme : ScriptableObject
    {
        [FormerlySerializedAs("_icon")] public Texture2D icon;
        [FormerlySerializedAs("_title")] public string title;
        [FormerlySerializedAs("_sections")] public Section[] sections;
        [FormerlySerializedAs("_loadedLayout")] public bool loadedLayout;

        [Serializable]
        public class Section
        {
            [FormerlySerializedAs("_heading")] public string heading;
            [FormerlySerializedAs("_text")] public string text;
            [FormerlySerializedAs("_linkText")] public string linkText;
            [FormerlySerializedAs("_url")] public string url;
        }
    }
}
