using UnityEngine;
using UnityEngine.Events;

namespace Plugins.AltoCityUIPack.Scripts.Slider
{
    public class SliderEnumsAltoCityUIPack : MonoBehaviour
    {

    }

    public enum SliderStartPoint
    {
        Left,
        Right,
        Top,
        Down
    }
    
    [System.Serializable]
    public class SliderEvent : UnityEvent<float> { }
}
