using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Nerds.Creative
{
    public class CreativeHand : MonoBehaviour
    {
        #region Variables
        private RectTransform _myTranform;
        #endregion

        #region Constructor
        public static void Launch()
        {
            Instantiate(Resources.Load<GameObject>("Nerds/[CreativeHand]"), FindObjectOfType<Canvas>().transform);
        }
        #endregion

        #region UnityMethods
        private void Start()
        {
            _myTranform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            FollowFinger();
        }
        #endregion

        #region Methods
        private void FollowFinger()
        {
            _myTranform.position = Vector3.MoveTowards(_myTranform.position, Input.mousePosition, 800f * Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
            {
                _myTranform.DOScale(0.8f, 0.1f);
                _myTranform.GetChild(0).DOLocalRotate(new Vector3(15f, 15f, 35f), 0.1f);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _myTranform.DOScale(1f, 0.1f);
                _myTranform.GetChild(0).DOLocalRotate(new Vector3(0f, 0f, 35f), 0.1f);
            }
        }
        #endregion
    }
}
