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
    
    public partial class IPPanel : UIObject
    {
        protected override void Awake()
        {
            base.Awake();
            this.autoBind();
            this.onAwake();
        }
        public void autoBind()
        {
            this.m_as_Image = this.GetComponent<Image>();
            this._IPInputField = this.transform.Find("IPInputField").GetComponent<InputField>();
            this._ConnectButton = this.transform.Find("ConnectButton").GetComponent<Button>();
            this._AddressText = this.transform.Find("AddressText").GetComponent<Text>();
        }
        private Main _parent;
        public Main parent
        {
            get
            {
                return this.transform.parent.parent.parent.GetComponent<Main>();
            }
        }
        [SerializeField()]
        private Image m_as_Image;
        public Image asImage
        {
            get
            {
                if ((this.m_as_Image == null))
                {
                    this.m_as_Image = this.GetComponent<Image>();
                }
                return this.m_as_Image;
            }
        }
        [SerializeField()]
        private InputField _IPInputField;
        public InputField IPInputField
        {
            get
            {
                if ((this._IPInputField == null))
                {
                    this._IPInputField = this.transform.Find("IPInputField").GetComponent<InputField>();
                }
                return this._IPInputField;
            }
        }
        [SerializeField()]
        private Button _ConnectButton;
        public Button ConnectButton
        {
            get
            {
                if ((this._ConnectButton == null))
                {
                    this._ConnectButton = this.transform.Find("ConnectButton").GetComponent<Button>();
                }
                return this._ConnectButton;
            }
        }
        [SerializeField()]
        private Text _AddressText;
        public Text AddressText
        {
            get
            {
                if ((this._AddressText == null))
                {
                    this._AddressText = this.transform.Find("AddressText").GetComponent<Text>();
                }
                return this._AddressText;
            }
        }
        partial void onAwake();
    }
}
