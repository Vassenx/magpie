using Animancer;
using UnityEngine;

namespace Magpie
{
    [CreateAssetMenu(fileName = "ProjectileSetting", menuName = "ScriptableObjects/ProjectileSettings", order = 1)]
    public class ProjectileSettings : ScriptableObject
    {
        public float baseDamage = 10f;
        public float speed;
        [Range(0, 75)] public float angle;
        public float maxLifeTimeSec;
        public float maxDistanceSqr;
        public SoloAnimation impactAnimation;
    }
}
