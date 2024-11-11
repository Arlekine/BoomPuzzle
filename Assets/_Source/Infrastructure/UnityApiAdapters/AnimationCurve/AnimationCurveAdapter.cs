using UnityEngine;

namespace Infrastructure
{
    public class AnimationCurveAdapter : IAnimationCurve
    {
        private AnimationCurve _curve;

        public AnimationCurveAdapter(AnimationCurve curve)
        {
            _curve = curve;
        }

        public float Evaluate(float time) => _curve.Evaluate(time);
    }
}