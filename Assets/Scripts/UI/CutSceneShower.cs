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
        [SerializeField] private CanvasGroup _flashLightScreen;
        [SerializeField] private CanvasGroup _cameraImageScreen;
        [SerializeField] private CanvasGroup _imageScreen;
        [SerializeField] private CanvasGroup _cameraScreen;
        [SerializeField] private Image _image;
        [SerializeField] private Image _cameraImage;
        [SerializeField] private List<CutSceneData> _cutScenes = new List< CutSceneData>();
        private InputControl _inputControl;

        [Inject]
        private void Construct(InputControl inputControl)
        {
            _inputControl = inputControl;
        }
        
        public void ShowCutscene(string cutSceneName)
        {
            if(cutSceneName=="Start")
                AudioManager.Instance.PlayAmbient("Start",0);
            
            _inputControl.BlockAllInput();
            CutSceneData data = _cutScenes.Find(x => x.Name == cutSceneName);
            _frontBlackScreen.alpha = 0f;
            _backBlackScreen.alpha = 1f;
            _imageScreen.alpha = 0f;
            _flashLightScreen.alpha = 0f;
            _cameraImageScreen.alpha = 0f;
            _cameraScreen.alpha = 0f;
            StartCoroutine(CutSceneProcess(data));

        }

        public void ShowPhotoCutscene(string cutSceneName)
        {
            _inputControl.BlockAllInput();
            CutSceneData data = _cutScenes.Find(x => x.Name == cutSceneName);
            _frontBlackScreen.alpha = 0f;
            _backBlackScreen.alpha = 1f;
            _imageScreen.alpha = 0f;
            _flashLightScreen.alpha = 0f;
            _cameraImageScreen.alpha = 0f;
            _cameraScreen.alpha = 1f;
            StartCoroutine(PhotoCutSceneProcess(data));
            
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

        private IEnumerator PhotoCutSceneProcess(CutSceneData data)
        {
            for (int i = 0; i < data.CutSceneSprites.Count; i++)
            {
                _cameraImage.sprite = data.CutSceneSprites[i];
                _flashLightScreen.alpha = 1f;
                _flashLightScreen.DOFade(0, _delay / 2);
                _cameraImageScreen.DOFade(1, _delay / 2);
                yield return new WaitForSeconds(_delay);
            }
            
            _frontBlackScreen.alpha = 0f;
            _backBlackScreen.alpha = 0f;
            _imageScreen.alpha = 0f;
            _cameraImageScreen.alpha = 0f;
            _cameraScreen.alpha = 0f;
            _inputControl.UnlockAllInput();
        }
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
