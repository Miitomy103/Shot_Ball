using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class InFrameObject : DragObject
    {
        protected Vector3 originalSize;

        protected bool inFrame = false;
        protected Frame frameScr;
        Frame FindAvailableFrame()
        {
            Vector2 mouseWorldPos = transform.position;

            Collider2D[] hits = Physics2D.OverlapPointAll(mouseWorldPos);

            foreach (var hit in hits)
            {
                if (hit.gameObject.TryGetComponent<Frame>(out var frame))
                {
                    if (frame.InObject == null)
                    {
                        return frame;
                    }
                }
            }
            return null;
        }

        protected bool FramePositionNow()
        {
            var frame = FindAvailableFrame();
            if (frame != null)
            {
                FrameSpriteIn(frame);
                return true;
            }
            return false;
        }

        protected bool IsFramePositionAvailable()
        {
            return FindAvailableFrame() != null;
        }


        public void FrameSpriteIn(Frame frame)
        {
            
            FrameItObject(frame.Sprite);

            Vector3 pos = frame.transform.position;
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);

            inFrame = true;
            frameScr = frame;
            frame.PutIn(gameObject);
        }
        protected abstract void FrameItObject(SpriteRenderer frameSprite);
        protected void FitSpriteInSquare(SpriteRenderer frameSprite, SpriteRenderer inSprite)
        {
            // フレームスプライトのワールドサイズ
            Vector2 frameSize = frameSprite.bounds.size;
            float squareSize = Mathf.Min(frameSize.x, frameSize.y) * 0.5f;

            // 自分のスプライトの「元の」サイズ（ローカルスケール前のワールド単位）
            Vector2 myOriginalSize = inSprite.sprite.rect.size / inSprite.sprite.pixelsPerUnit;

            float maxSide = Mathf.Max(myOriginalSize.x, myOriginalSize.y);
            if (maxSide == 0)
            {
                transform.localScale = Vector3.zero;
                return;
            }

            float scaleFactor = squareSize / maxSide;

            // 元のローカルスケール（変更前）をベースにスケーリング
            transform.localScale = Vector3.one * scaleFactor;
        }
    }
}
