using UnityEngine;
using System.Collections;
using JazzDev.Executor;
using JazzDev.Easing;
using UnityEngine.UI;
using System.Collections.Generic;

public static class Unity3dExtensions
{

    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static Color SetAlpha(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }

    public static void Vec3ToColor(this ref Color color, Vector3 value)
    {
        color = new Color(value.x, value.y, value.z, color.a);
    }

    public static void Vec4ToColor(this ref Color color, Vector4 value)
    {
        color = new Color(value.x, value.y, value.z, value.w);
    }



    public static void AnimateAlpha(this CanvasGroup canvas, float to, float duration, float delay = 0f, EaseStyle ease = EaseStyle.StrongEaseOut)
    {
        string uniqueName = canvas.gameObject.GetInstanceID().ToString();
        ExecutorBase.GetManager().Destroy(uniqueName);
        float from = canvas.alpha;
        Ease _ease = EaseMethods.GetEase(ease);
        ExecEvaluation evaluator = new ExecEvaluation(duration, delay, EExecutorDirection.Forward, uniqueName);
        evaluator.SetUpdate(executor => { canvas.alpha = EaseMethods.EasedLerp(_ease, from, to, executor.Evaluation); });
        evaluator.Run();
    }
    public static void AnimateAlpha(this Graphic graphic, float to, float duration, float delay = 0f, EaseStyle ease = EaseStyle.StrongEaseOut)
    {
        string uniqueName = graphic.gameObject.GetInstanceID().ToString();
        ExecutorBase.GetManager().Destroy(uniqueName);
        float from = graphic.canvasRenderer.GetAlpha();
        Ease _ease = EaseMethods.GetEase(ease);
        ExecEvaluation evaluator = new ExecEvaluation(duration, delay, EExecutorDirection.Forward, uniqueName);
        evaluator.SetUpdate(executor => { graphic.canvasRenderer.SetAlpha(EaseMethods.EasedLerp(_ease, from, to, executor.Evaluation)); });
        evaluator.Run();
    }
    public static void AnimateColor(this Graphic graphic, Color to, float duration, float delay = 0f, EaseStyle ease = EaseStyle.StrongEaseOut)
    {
        string uniqueName = graphic.gameObject.GetInstanceID().ToString();
        ExecutorBase.GetManager().Destroy(uniqueName);
        Color from = graphic.color;
        Ease _ease = EaseMethods.GetEase(ease);
        ExecEvaluation evaluator = new ExecEvaluation(duration, delay, EExecutorDirection.Forward, uniqueName);
        evaluator.SetUpdate(executor =>
        {
            graphic.color = Color.Lerp(from, to, EaseMethods.EasedLerp(_ease, 0, 1, executor.Evaluation));
        });
        evaluator.Run();
    }


}
