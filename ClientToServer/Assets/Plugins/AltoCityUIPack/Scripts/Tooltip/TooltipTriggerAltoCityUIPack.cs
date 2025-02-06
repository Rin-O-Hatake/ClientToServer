using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
// using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Plugins.AltoCityUIPack.Scripts.Dropdown;

namespace Plugins.AltoCityUIPack.Scripts.Tooltip
{
    public sealed class TooltipTriggerAltoCityUIPack : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        #region Fields

        [Title("Tooltip")]
        [SerializeField] private string _title;
        [SerializeField, TextArea] private string _description;

        [Space(10)]
        [SerializeField] private TooltipTypeAltoCityPackUI _typeTooltip;
        [SerializeField] private PositionTooltipEnums _positionTooltip;
        [SerializeField] private bool _hideOnClick;

        [Space(10)]
        [SerializeField] private float _delayToShow = 1;
        
        private CancellationTokenSource _hideCancellationTokenSource = new();

        #endregion

        #region Event Functions

        private void OnDisable()
            => Hide();

        private void OnDestroy()
            => _hideCancellationTokenSource.Dispose();

        #endregion
        
        #region IPointers

        public async void OnPointerEnter(PointerEventData eventData)
        {
            // try
            // {
            //     await UniTask.Delay(TimeSpan.FromSeconds(_delayToShow),
            //         cancellationToken: _hideCancellationTokenSource.Token);
            // }
            // catch (OperationCanceledException)
            // {
            //     return;
            // }
            //
            // TooltipSystemCustomAltoCityPack.Show(_description, _typeTooltip, _positionTooltip, _title);
        }

        public void OnPointerExit(PointerEventData eventData)
            => Hide();

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_hideOnClick)
            {
                Hide();
            }
        }
        
        #endregion

        private void Hide()
        {
            TooltipSystemCustomAltoCityPack.Hide();
            _hideCancellationTokenSource.Cancel();
            _hideCancellationTokenSource = new CancellationTokenSource();
        }
    }
}