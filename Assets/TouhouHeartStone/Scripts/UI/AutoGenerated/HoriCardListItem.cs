//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UI
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using BJSYGameCore.UI;
    
    public partial class HoriCardListItem : UIObject
    {
        protected override void Awake()
        {
            base.Awake();
            this.autoBind();
            this.onAwake();
        }
        public void autoBind()
        {
            this.m_as_Button = this.GetComponent<Button>();
            this._Card = this.transform.Find("Card").GetComponent<Card>();
        }
        private Main _parent;
        public Main parent
        {
            get
            {
                return this.transform.parent.parent.parent.parent.parent.parent.GetComponent<Main>();
            }
        }
        [SerializeField()]
        private Button m_as_Button;
        public Button asButton
        {
            get
            {
                if ((this.m_as_Button == null))
                {
                    this.m_as_Button = this.GetComponent<Button>();
                }
                return this.m_as_Button;
            }
        }
        [SerializeField()]
        private Card _Card;
        public Card Card
        {
            get
            {
                if ((this._Card == null))
                {
                    this._Card = this.transform.Find("Card").GetComponent<Card>();
                }
                return this._Card;
            }
        }
        partial void onAwake();
    }
}
