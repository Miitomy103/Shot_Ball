using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// BallÇ™êGÇÍÇΩÇÁÅAàÍíËéûä‘í‚é~ÇµÇƒÇ©ÇÁéwíËÇµÇΩï˚å¸Ç…îÚÇŒÇ∑ÉMÉ~ÉbÉN
    /// </summary>
    public class BallLauncher : AreaBase
    {
        [SerializeField] float launchPower = 40f;
        [SerializeField] Vector2 launchDirection;
        const float coolTime = 0.5f;

        protected override bool HasDirection => true;
        protected override Vector2 Direction => launchDirection;

        bool inBall;
        Transform child;

        [SerializeField] AudioSource reloadSound;
        [SerializeField] AudioSource launchSound;
        protected override void Start()
        {
            child = transform.Find("Arrow");
            child.localRotation = DirectionAngle();
            base.Start();
        }
        protected override void BallEnter(Ball other)
        {
            base.BallEnter(other);
            if (inBall) return;
            StartCoroutine(Coroutine(other.gameObject.GetComponent<Rigidbody2D>()));
        }
        protected override void Rotation()
        {
            base.Rotation();
            child.localRotation = DirectionAngle();
        }
        IEnumerator Coroutine(Rigidbody2D rb)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            inBall = true;
            rb.velocity = Vector2.zero;
            rb.gameObject.transform.position = transform.position;
            reloadSound.Play();

            yield return new WaitForSeconds(coolTime);

            inBall = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            Vector2 worldForce = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Direction;
            rb.AddForce(launchPower * 10 * worldForce);
            launchSound.Play();
        }

        protected override string StringData()
        {
            return $"{launchPower},{launchDirection.x},{launchDirection.y}";
        }
        public override void LoadData(string data)
        {
            base.LoadData(data);
            string[] splitData = data.Split(',');
            launchPower = float.Parse(splitData[0]);
            launchDirection = new Vector2(float.Parse(splitData[1]), float.Parse(splitData[2]));
        }

        public override void LoadData(StageBlockData data)
        {
            if (data.floatParameters.TryGetValue("LaunchPower", out float powerValue))
            {
                launchPower = powerValue;
            }
            else
            {
                Debug.LogWarning("LaunchPower parameter not found in BallLauncher data.");
            }
            if (data.vector2Parameters.TryGetValue("LaunchDirection", out Vector2 directionValue))
            {
                launchDirection = directionValue;
            }
            else
            {
                Debug.LogWarning("LaunchDirection parameter not found in BallLauncher data.");
            }
        }
    }
}
