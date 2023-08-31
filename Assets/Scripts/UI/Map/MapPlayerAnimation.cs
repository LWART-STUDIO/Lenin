using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Teleport.Map
{
    public class MapPlayerAnimation : MonoBehaviour
    {

        // References to sprites
        public Sprite[] upSprites;
        public Sprite[] downSprites;
        public Sprite[] sideSprites;
        public Sprite[] idleSprites;
        public Sprite[] backIdleSprites;

        // Field for frame change speed
        public float frameSpeed = 0.1f;

        // Current set of sprites and index of the current frame
        public Sprite[] currentSprites;
        private int currentFrameIndex = 0;

        private Image Renderer;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _position;
        private bool _animate = false;
        
        private void Start()
        {
            Renderer = transform.GetChild(0).GetComponent<Image>();
            _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
            _position = transform.position;
        }

        private void Update()
        {
            if(!_animate)
                return;
            Animate((Vector2)transform.position-_position);
            _position = transform.position;
        }

        public void Go()
        {
            _animate = true;
        }

        // Animation method, taking movement direction
        public void Animate(Vector2 direction)
        {
            SetCurrentSprites(direction);
            UpdateSprite();
        }

        public void Animate(string direction)
        {
            // use idle sprites
            switch (direction)
            {
                case "front":
                    currentSprites = idleSprites;
                    break;
                case "back":
                    currentSprites = backIdleSprites;
                    break;
            }

            UpdateSprite();
        }

        private void SetCurrentSprites(Vector2 direction)
        {
            // Determine current sprites based on movement direction
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                currentSprites = sideSprites;
                SetSpriteRendererFlipX(direction.x);
            }
            else
            {
                currentSprites = (direction.y > 0) ? upSprites : downSprites;
            }
        }

        private void SetSpriteRendererFlipX(float directionX)
        {
            if(Renderer!=null)
                Renderer.rectTransform.localScale = new Vector2((!(directionX < 0) ?
                    -Math.Abs(Renderer.rectTransform.localScale.x) :
                    Math.Abs(Renderer.rectTransform.localScale.x)),
                    Math.Abs(Renderer.rectTransform.localScale.y));
            if(_spriteRenderer!=null)
                _spriteRenderer.transform.localScale = new Vector2((!(directionX < 0) ?
                        -Math.Abs(_spriteRenderer.transform.localScale.x) :
                        Math.Abs(_spriteRenderer.transform.localScale.x)),
                    Math.Abs(_spriteRenderer.transform.localScale.y));
            
        }

        private void UpdateSprite()
        {
            // Update sprite every frameSpeed seconds
            currentFrameIndex = (int)(Time.time / frameSpeed) % currentSprites.Length;
            if(Renderer!=null)
                Renderer.sprite = currentSprites[currentFrameIndex];
            if(_spriteRenderer!=null)
                _spriteRenderer.sprite = currentSprites[currentFrameIndex];
        }
    }
}
