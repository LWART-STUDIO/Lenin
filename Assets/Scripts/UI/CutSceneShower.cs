using System.Collections;
using System.Collections.Generic;
using CustomInput;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    //REMOVE MAGIC
    public class CutSceneShower : MonoBehaviour
    {
        [SerializeField] private float _delay;
        [SerializeField] private CanvasGroup _backBlackScreen;
        [SerializeField] private CanvasGroup _frontBlackScreen;
        [SerializeField] private CanvasGroup _imageScreen;
        [SerializeField] private Image _image;
        [SerializeField] private List<CutSceneData> _cutScenes = new List< CutSceneData>();
        private InputControl _inputControl;

        [Inject]
        private void Construct(InputControl inputControl)
        {
            _inputControl = inputControl;
        }
        public void ShowCutscene(string cutSceneName)
        {
            _inputControl.BlockAllInput();
            CutSceneData data = _cutScenes.Find(x => x.Name == cutSceneName);
            _frontBlackScreen.alpha = 0f;
            _backBlackScreen.alpha = 1f;
            _imageScreen.alpha = 0f;
            StartCoroutine(CutSceneProcess(data));

        }

        public void ShowBlackLoadingScreen()
        {
            _frontBlackScreen.DOFade(1, _delay / 3).SetLoops(2,LoopType.Yoyo)
                .OnComplete(_inputControl.UnlockAllInput);
        }

        #if UNITY_EDITOR
        [NaughtyAttributes.Button()]
        private void ShowStartCutscene()
        {
            ShowCutscene("Start");
        }
        #endif
        private IEnumerator CutSceneProcess(CutSceneData data)
        {
            for (int i = 0; i < data.CutSceneSprites.Count; i++)
            {
                _image.sprite = data.CutSceneSprites[i];
                _imageScreen.DOFade(1, _delay / 2);
                yield return new WaitForSeconds(_delay);
                _imageScreen.DOFade(0, _delay / 2);
                yield return new WaitForSeconds(_delay/2);
            }
            _frontBlackScreen.alpha = 0f;
            _backBlackScreen.alpha = 0f;
            _imageScreen.alpha = 0f;
            _inputControl.UnlockAllInput();
        }
    }
}
