using DG.Tweening;
using RedRats.Systems.LiteFeel.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

public class LFUICanvasGroupEffect : LFEffectTweenBase
{
    [SerializeField, InfoBox("Missing target", InfoMessageType.Error, "@target == null")] private CanvasGroup target;
    [SerializeField] private ImageFadeType fade = ImageFadeType.FadeIn;
    [SerializeField] private AnimationCurve fadeCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
    
    private float startAlpha;
    
    protected override void Initialize()
    {
        // Nothing to do here.
    }

    protected override void SetBeginState()
    {
       // Nothing to do here.
    }

    protected override void SetupTweens()
    {
        Tween tween = DOTween.To(() => target.alpha, x => target.alpha = x, GetTargetValue(), duration);
        AddTweenToSequence(tween, fadeCurve);
    }

    protected override void ResetTargetState() => target.alpha = startAlpha;
    protected override void UpdateStartingValues() => startAlpha = target.alpha;

    private float GetTargetValue() => (fade == ImageFadeType.FadeIn) ? 1f : 0f;

    protected override string FeedbackColor { get => "#FF8251"; }
}
