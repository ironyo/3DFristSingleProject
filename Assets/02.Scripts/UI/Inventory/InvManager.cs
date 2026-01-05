using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InvManager : MonoBehaviour
{
    private bool _isStillPush;
    [SerializeField] List<InvPage> _invPageCompo;
    private List<RectTransform> _invTrans = new();
    private List<Image> _invImg = new();

    private HorizontalLayoutGroup _layoutGroupCompo;

    public int NowSelectNum { get; private set; } = 0;

    private void Awake()
    {
        _layoutGroupCompo = GetComponent<HorizontalLayoutGroup>();
        for (int i = 0; i < _invPageCompo.Count; i++)
        {
            _invTrans.Add(_invPageCompo[i].gameObject.GetComponent<RectTransform>());
            _invImg.Add(_invPageCompo[i].gameObject.GetComponentInChildren<Image>());
        }
    }
    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            PushButton(1);
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
            PushButton(2);
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
            PushButton(3);

    }

    private void PushButton(int num)
    {
        if (_isStillPush) return;

        _isStillPush = true;

        if (_invPageCompo[num - 1].IsHaveItem)
        {
            if (NowSelectNum == num)
            {
                ClearInvTransform(0);
                NowSelectNum = 0;
                _isStillPush = false;
                return;
            }


            ClearInvTransform(num-1);
            NowSelectNum = num;
            _invPageCompo[num - 1].InvUISet(true);

            _invImg[num - 1].DOFade(1, 0.2f);
            SelectMoveDot(num);
            for (int i = 0; i < num - 1; i++)
            {
                _invPageCompo[i].MoveInv(true);
            }
        }

        else
        {
            for (int i = 0; i < num - 1; i++)
            {
                _invPageCompo[i].MoveInv(true);
            }
            _invTrans[num - 1].DOScale(1.2f, 0.2f).OnComplete(() =>
            {
                for (int i = 0; i < num - 1; i++)
                {
                    _invPageCompo[i].MoveInv(false);
                }
                _invTrans[num - 1].DOScale(1f, 0.1f).OnComplete(() =>
                {
                    _isStillPush = false;
                });
            });
        }
    }

    private void SelectMoveDot(int num)
    {
        Transform target = _invTrans[num - 1];

        Sequence seq = DOTween.Sequence();

        seq.Append(
            target.DOScale(1.35f, 0.18f)
                  .SetEase(Ease.OutCubic)
        );

        seq.Append(
            target.DOScale(1.25f, 0.08f)
        );

        seq.Append(
           target.DOScale(1.3f, 0.12f)
                  .SetEase(Ease.OutBack, 2.2f)
        );

        seq.OnComplete(() =>
        {
            _isStillPush = false;
        });
    }

    private void ClearInvTransform(int notNum)
    {
        for (int i = 0; i < _invPageCompo.Count; i++)
        {
            if(notNum == 0)
            {
                _invImg[i].DOFade(0.33f, 0.2f);
                _invTrans[i].DOScale(1, 0.2f);
                _invPageCompo[i].MoveInv(false);
                _invPageCompo[i].InvUISet(false);
            }
            else if(notNum != i)
            {
                _invImg[i].DOFade(0.33f, 0.2f);
                _invTrans[i].DOScale(1, 0.2f);
                _invPageCompo[i].MoveInv(false);
                _invPageCompo[i].InvUISet(false);
            }
        }
    }
}
