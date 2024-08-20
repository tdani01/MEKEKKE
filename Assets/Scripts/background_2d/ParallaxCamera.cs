using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;

    private float _pos;

    void Start()
    {
        _pos = transform.position.x;
    }

    void Update()
    {
        if (transform.position.x != _pos)
        {
            if (onCameraTranslate != null)
            {
                float delta = _pos - transform.position.x;
                onCameraTranslate(delta);
            }

            _pos = transform.position.x;
        }
    }
}
