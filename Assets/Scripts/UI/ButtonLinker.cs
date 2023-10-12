using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonLinker : MonoBehaviour
    {
        [field: SerializeField] public Image Image { get; private set; }
        [field: SerializeField] public Button Button { get; private set; }
    }
}