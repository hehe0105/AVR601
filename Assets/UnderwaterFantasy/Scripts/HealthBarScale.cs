using UnityEngine;

public class HealthBarScale : MonoBehaviour
{
    public RectTransform fillRect;

    public Vector3 worldOffset = new Vector3(0f,0.7f,0f);

    Transform target;
    Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (!fillRect) {
            var t = transform.Find("BG/Fill");
            if (!t) t = transform.Find("Fill");
            if (t) fillRect = t as RectTransform;
        }
        target = transform.parent;
        cam = Camera.main;

        if (fillRect) fillRect.pivot = new Vector2(0f,0.5f);
    }

    void LateUpdate()
    {
        if (target) transform.position = target.position + worldOffset;
        if (cam) transform.forward = -cam.transform.forward;//面向相机
    }
    //0~1；0=空血，1=满血
    public void SetFill01(float normalized) {
        if (!fillRect) return;
        normalized = Mathf.Clamp01(normalized);
        var s = fillRect.localScale;
        s.x = (normalized <= 0f) ? 0.0001f : normalized;//避免0选取问题
        fillRect.localScale = s;
    }

    public void SetHealth(int current, int max) {
        float n = (max <= 0) ? 0f : (float)current / max;
        SetFill01(n);
    }
}
