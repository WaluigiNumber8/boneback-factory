using System.Collections;
using DG.Tweening;
using RedRats.Systems.LiteFeel.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

public class LFUICanvasGroupEffect : LFEffectTweenBase
{
    [SerializeField, Required] private CanvasGroup target;
    [SerializeField] private ImageFadeType fade = ImageFadeType.FadeIn;
    [SerializeField, ShowIf("fade", ImageFadeType.Custom)] private float beginAlpha = 0;
    [SerializeField, ShowIf("fade", ImageFadeType.Custom)] private float targetAlpha = 1;
    [SerializeField] private AnimationCurve fadeCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField] private bool blockRaycastsDuringPlay;
    
    private float startAlpha;
    
    protected override void SetBeginState()
    {
        if (target == null) return;
        target.alpha = (fade == ImageFadeType.Custom) ? beginAlpha : (fade == ImageFadeType.FadeIn) ? 0f : 1f;
        if (!blockRaycastsDuringPlay) return;
        if (gameObject.activeInHierarchy == false) return;
        StartCoroutine(DelayCoroutine());
        IEnumerator DelayCoroutine()
        {
            yield return new WaitForSeconds(0.1f);
            target.blocksRaycasts = true;
        }
    }

    protected override void SetupTweens()
    {
        Tween tween = DOTween.To(() => target.alpha, x => target.alpha = x, GetTargetValue(), Duration);
        if (blockRaycastsDuringPlay) tween.OnComplete(() => target.blocksRaycasts = false);
        AddTweenToSequence(tween, fadeCurve);
    }

    protected override void ResetTargetState() => target.alpha = startAlpha;

    protected override void UpdateStartingValues() => startAlpha = target.alpha;

    private float GetTargetValue() => (fade == ImageFadeType.Custom) ? targetAlpha : (fade == ImageFadeType.FadeIn) ? 1f : 0f;

    protected override string FeedbackColor { get => "#FF8251"; }
}
