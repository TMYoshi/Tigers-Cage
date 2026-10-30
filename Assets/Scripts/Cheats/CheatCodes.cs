using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Cheatcodes : MonoBehaviour
{
    public string InputQueue = "Fish";

    [Tooltip("Has to be 7 letters")]
    public List<CheatCodesWithEvents> Cheats;
    [SerializeField] AudioClip cheatJingle;

    private void Start()
    {
        Keyboard.current.onTextInput -= OnTextInput;
        Keyboard.current.onTextInput += OnTextInput;
    }

    private void OnDisable()
    {
        Keyboard.current.onTextInput -= OnTextInput;
    }

    private void OnTextInput(char _input)
    {
        if(InputQueue.Length >= 7)
            InputQueue = InputQueue[1..] + _input;
        else
            InputQueue += _input;

        UnityEvent currentEvent = Cheats
            .FirstOrDefault(currentCheat => currentCheat.Codes == InputQueue)
            .Events;

        if(currentEvent != null)
        {
            SFXManager.Instance.PlaySFXClip(cheatJingle);
            currentEvent.Invoke();
        }
    }
}

[System.Serializable]
public struct CheatCodesWithEvents
{
    public string Codes;
    public UnityEvent Events;
}
