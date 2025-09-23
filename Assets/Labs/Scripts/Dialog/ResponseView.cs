using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class ResponseView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _view;

        public UnityEvent OnSelectSignal = new();

        public void Show(string text)
        {
            StartCoroutine(DialogView.PrintTextRoutine(_view, text));
        }

        public void Select()
        {
            OnSelectSignal.Invoke();
        }
    }
}
