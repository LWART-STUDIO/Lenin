using System;
using System.Collections.Generic;
using DG.Tweening;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Serialization;
using DialogueManager = Dialogues.DialogueManager;
using Random = UnityEngine.Random;

namespace NPC
{
    public class NPCMover : MonoBehaviour
    {
        public bool ReachedDestination;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private NPCAnimation _npcAnimation;
        private bool _startPatrol;
        private Tween _moveTween;
        private Vector3 _destination;
        
        public void StartPatrol()
        {
            StartPatrolChange();
        }

        private void StartPatrolChange()
        {
            _startPatrol = !_startPatrol;
            _npcAnimation.ChangeAnimate();
        }

        private void FixedUpdate()
        {
            
            if(!_startPatrol)
                return;
            if(_moveTween == null)
                _moveTween = _rigidbody2D.DOMove(_destination, 3f).SetSpeedBased().SetEase(Ease.Linear).OnComplete(UpdatePoint);
            Vector2 direction = _destination-transform.position;
            if (direction != Vector2.zero)
                _npcAnimation.Animate(direction);
            else
                _npcAnimation.Animate("front");
        }

        public void PauseMove(Transform actor)
        {
            _moveTween.Pause();
            StartPatrolChange();
            _npcAnimation.Animate("front");
            
        }
        
        public void ContinueMove(Transform actor)
        {
            _moveTween.Play();
            StartPatrolChange();
            DialogueManager.instance.conversationEnded-=ContinueMove;
        }
        public void UpdatePoint()
        {
            ReachedDestination = true;
            _moveTween = null;
        }

        public void SetDestination(Vector3 destination)
        {
            _destination = destination;
            ReachedDestination = false;
        }
    }
}
