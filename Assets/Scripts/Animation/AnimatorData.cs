using UnityEngine;

public static class AnimatorData
{
    public static class Parameters
    {
        public static readonly int Jump = Animator.StringToHash(nameof(Jump));
        public static readonly int Fall = Animator.StringToHash(nameof(Fall));
    }
}
