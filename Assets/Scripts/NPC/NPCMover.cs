using System;
using System.Collections.Generic;
using DG.Tweening;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NPC
{
    public class NPCMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private NPCAnimation _npcAnimation;
        private List<Transform> _patrolPoints;
        private bool _startPatrol;
        private Transform _currentPoint;
        private Tween _moveTween;

        public void StartPatrol(List<Transform> patrolPoints)
        {
            _patrolPoints = patrolPoints;
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
            if (_currentPoint == null)
                _currentPoint = _patrolPoints[Random.Range(0, _patrolPoints.Count)];
            if(_moveTween == null)
                _moveTween = _rigidbody2D.DOMove(_currentPoint.position, 3f).SetSpeedBased().SetEase(Ease.Linear).OnComplete(UpdatePoint);
            Vector2 direction = _currentPoint.position-transform.position;
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
        private void UpdatePoint()
        {
            _currentPoint = null;
            _moveTween = null;
        }
    }
}
