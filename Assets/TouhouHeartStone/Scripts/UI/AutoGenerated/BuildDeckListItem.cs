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
    
    public partial class BuildDeckListItem : UIObject
    {
        protected override void Awake()
        {
            base.Awake();
            this.autoBind();
            this.onAwake();
        }
        public void autoBind()
        {
            this._MaskImage = this.transform.Find("MaskImage").GetComponent<Image>();
            this._Mask = this.transform.Find("Mask").GetComponent<Mask>();
            this._Image = this.transform.Find("Mask").Find("Image").GetComponent<Image>();
            this._FadeImage = this.transform.Find("Fade").GetComponent<Image>();
            this._Text = this.transform.Find("Text").GetComponent<Text>();
            this._NumText = this.transform.Find("NumText").GetComponent<Text>();
        }
        [SerializeField()]
        private Image _MaskImage;
        public Image MaskImage
        {
            get
            {
                if ((this._MaskImage == null))
                {
                    this._MaskImage = this.transform.Find("MaskImage").GetComponent<Image>();
                }
                return this._MaskImage;
            }
        }
        [SerializeField()]
        private Mask _Mask;
        public Mask Mask
        {
            get
            {
                if ((this._Mask == null))
                {
                    this._Mask = this.transform.Find("Mask").GetComponent<Mask>();
                }
                return this._Mask;
            }
        }
        [SerializeField()]
        private Image _Image;
        public Image Image
        {
            get
            {
                if ((this._Image == null))
                {
                    this._Image = this.transform.Find("Mask").Find("Image").GetComponent<Image>();
                }
                return this._Image;
            }
        }
        [SerializeField()]
        private Image _FadeImage;
        public Image FadeImage
        {
            get
            {
                if ((this._FadeImage == null))
                {
                    this._FadeImage = this.transform.Find("Fade").GetComponent<Image>();
                }
                return this._FadeImage;
            }
        }
        [SerializeField()]
        private Text _Text;
        public Text Text
        {
            get
            {
                if ((this._Text == null))
                {
                    this._Text = this.transform.Find("Text").GetComponent<Text>();
                }
                return this._Text;
            }
        }
        [SerializeField()]
        private Text _NumText;
        public Text NumText
        {
            get
            {
                if ((this._NumText == null))
                {
                    this._NumText = this.transform.Find("NumText").GetComponent<Text>();
                }
                return this._NumText;
            }
        }
        partial void onAwake();
    }
}
