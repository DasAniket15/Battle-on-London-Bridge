using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    // Edit Asset references in the prefab Assets/Resources/PersonalAssets
    public class Assets : MonoBehaviour
    {
        private static Assets _i;


        public static Assets i
        {
            get
            {
                if (_i == null) _i = Instantiate(Resources.Load<Assets>("PersonalAssets"));

                return _i;
            }
        }


        public Sprite s_White;
        public Sprite s_Circle;

        public Material m_White;
    }
}
