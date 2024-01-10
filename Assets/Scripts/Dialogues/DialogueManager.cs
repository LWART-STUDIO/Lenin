using Aarthificial.Typewriter;
using Aarthificial.Typewriter.Entries;
using UnityEngine;

namespace Dialogues
{
    public class DialogueManager : MonoBehaviour
    {
        private void OnEnable() {
            TypewriterDatabase.Instance.AddListener(HandleTypewriterEvent);
            _skipButton.Clicked += HandleSkipClicked;
        }
        private void OnDisable() {
            TypewriterDatabase.Instance.RemoveListener(HandleTypewriterEvent);
            _skipButton.Clicked -= HandleSkipClicked;
        }
        
        private void HandleTypewriterEvent(
            BaseEntry entry,
            ITypewriterContext context
        ) {
            if (!IsActive && entry is DialogueEntry textEntry) {
                _currentEntry = textEntry;
                _currentContext = context;
                Begin();
            }
        }
        
    }
}
