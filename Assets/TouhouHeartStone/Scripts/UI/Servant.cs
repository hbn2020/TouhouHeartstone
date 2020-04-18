﻿using UnityEngine.EventSystems;
using UnityEngine;
using TouhouHeartstone;

namespace UI
{
    partial class Servant : IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public TouhouCardEngine.Card card { get; private set; }
        [SerializeField]
        AnimationCurve _attackAnimationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        public AnimationCurve attackAnimationCurve
        {
            get { return _attackAnimationCurve; }
        }
        public void update(THHPlayer player, TouhouCardEngine.Card card, CardSkinData skin)
        {
            this.card = card;

            Table table = GetComponentInParent<Table>();
            if (skin != null)
            {
                Image.sprite = skin.image;
            }
            AttackText.text = card.getAttack().ToString();
            HpText.text = card.getCurrentLife().ToString();
            if (table.player == player && table.game.currentPlayer == player && card.canAttack())
                CanAttackController = CanAttack.True;
            else
                CanAttackController = CanAttack.False;
        }
        [SerializeField]
        float _attackThreshold = 70;
        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            if (!card.canAttack())
                return;
        }
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (!card.canAttack())
                return;
            Table table = GetComponentInParent<Table>();
            //拉动距离也应该有一个阈值
            if (Vector2.Distance(rectTransform.position, eventData.position) > _attackThreshold)
            {
                //播放一个变大的动画？
                rectTransform.localScale = Vector3.one * 1.1f;
                //显示指针
                table.AttackArrowImage.display();
                table.AttackArrowImage.rectTransform.position = rectTransform.position;
                //移动指针
                table.AttackArrowImage.rectTransform.eulerAngles = new Vector3(0, 0,
                    Vector2.Angle(rectTransform.position, eventData.position));
                table.AttackArrowImage.rectTransform.up = ((Vector3)eventData.position - rectTransform.position).normalized;
                table.AttackArrowImage.rectTransform.sizeDelta = new Vector2(
                    table.AttackArrowImage.rectTransform.sizeDelta.x,
                    Vector2.Distance(rectTransform.position, eventData.position) / GetComponentInParent<Canvas>().transform.localScale.y);
                //高亮标记所有敌人
                THHPlayer opponent = table.game.getOpponent(table.player);
                if (card.isAttackable(table.game, table.player, opponent.master))
                {

                }
                else
                {

                }
                foreach (var servant in table.EnemyFieldList)
                {
                    if (card.isAttackable(table.game, table.player, servant.card))
                    {

                    }
                    else
                    {

                    }
                }
            }
            else
            {
                cancelAttack(table);
            }
        }
        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (!card.canAttack())
                return;
            Table table = GetComponentInParent<Table>();
            //如果在随从上面
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Servant>() is Servant targetServant)
                {
                    if (card.isAttackable(table.game, table.player, targetServant.card))
                    {
                        table.player.cmdAttack(table.game, card, targetServant.card);
                    }
                }
                else if (eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Master>() is Master targetMaster)
                {
                    if (card.isAttackable(table.game, table.player, targetMaster.card))
                    {
                        table.player.cmdAttack(table.game, card, targetMaster.card);
                    }
                }
            }
            //取消选中和攻击
            cancelAttack(table);
        }
        private void cancelAttack(Table table)
        {
            rectTransform.localScale = Vector3.one;
            table.AttackArrowImage.hide();
        }
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            displayLargeCard(eventData.position.x < Screen.width / 2);
        }
        private void displayLargeCard(bool isRight)
        {
            Table table = GetComponentInParent<Table>();
            if (isRight)
                table.LargeCard.rectTransform.localPosition = new Vector3(250, 0);
            else
                table.LargeCard.rectTransform.localPosition = new Vector3(-250, 0);
            table.LargeCard.display();
            table.LargeCard.update(card, table.getSkin(card));
        }
        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            hideLargeCard();
        }
        private void hideLargeCard()
        {
            Table table = GetComponentInParent<Table>();
            table.LargeCard.hide();
        }
    }
}