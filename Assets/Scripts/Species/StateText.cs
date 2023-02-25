using System;
using Species.States;
using TMPro;
using UnityEngine;

namespace Species
{
    public class StateText : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            GetComponentInParent<Species>().StateMachine.OnStateChanged += StateMachineOnOnStateChanged();
        }

        private Action<IState> StateMachineOnOnStateChanged()
        {
            return state =>
            {
                _text.SetText(state.ToString());
            };
        }
    }
}
