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
    
    public partial class RoomScrollViewItem : UIObject
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
            this._RoomList = this.transform.Find("RoomList").GetComponent<RoomList>();
        }
        private RoomScrollView _parent;
        public RoomScrollView parent
        {
            get
            {
                return this.transform.parent.GetComponent<RoomScrollView>();
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
        private RoomList _RoomList;
        public RoomList RoomList
        {
            get
            {
                if ((this._RoomList == null))
                {
                    this._RoomList = this.transform.Find("RoomList").GetComponent<RoomList>();
                }
                return this._RoomList;
            }
        }
        partial void onAwake();
    }
}