using UnityEngine;

namespace StitchApp
{
    /**
     * MochiModelSettings
     * 
     * ScriptableObject to hold the aesthetic configuration for the Mochi mascot.
     * This defines colors, glow intensities, and feature toggles.
     */
    [CreateAssetMenu(fileName = "NewMochiSettings", menuName = "Mochi Galaxy/Mochi Settings")]
    public class MochiModelSettings : ScriptableObject
    {
        [Header("Colors")]
        public Color skinTone = new Color(0.7f, 0.85f, 1.0f); // Default Baby Blue
        public Color earColor = new Color(0.9f, 0.7f, 1.0f); // Lavender
        public Color blushColor = new Color(1.0f, 0.7f, 0.8f, 0.4f);
        
        [Header("Aesthetics")]
        public float glowIntensity = 1.2f;
        public bool hasStarFreckles = true;
        
        [Header("Animation")]
        public float idleBounceSpeed = 1.0f;
        public float idleBounceAmount = 0.05f;
    }
}
