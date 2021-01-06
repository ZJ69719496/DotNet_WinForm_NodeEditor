﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ST.Library.UI
{
    public class STNodeControl
    {
        private STNode _Owner;

        public STNode Owner {
            get { return _Owner; }
            internal set { _Owner = value; }
        }

        private int _Left;

        public int Left {
            get { return _Left; }
            set {
                _Left = value;
                this.OnMove(new EventArgs());
                this.Invalidate();
            }
        }

        private int _Top;

        public int Top {
            get { return _Top; }
            set {
                _Top = value;
                this.OnMove(new EventArgs());
                this.Invalidate();
            }
        }

        private int _Width;

        public int Width {
            get { return _Width; }
            set {
                _Width = value;
                this.OnResize(new EventArgs());
                this.Invalidate();
            }
        }

        private int _Height;

        public int Height {
            get { return _Height; }
            set {
                _Height = value;
                this.OnResize(new EventArgs());
                this.Invalidate();
            }
        }

        public Point Location {
            get { return new Point(this._Left, this._Top); }
        }
        public Size Size {
            get { return new Size(this._Width, this._Height); }
        }
        public Rectangle DisplayRectangle {
            get { return new Rectangle(this._Left, this._Top, this._Width, this._Height); }
        }
        public Rectangle ClientRectangle {
            get { return new Rectangle(0, 0, this._Width, this._Height); }
        }

        private Color _BackColor = Color.FromArgb(127, 0, 0, 0);

        public Color BackColor {
            get { return _BackColor; }
            set {
                _BackColor = value;
                this.Invalidate();
            }
        }

        private Color _ForeColor = Color.White;

        public Color ForeColor {
            get { return _ForeColor; }
            set {
                _ForeColor = value;
                this.Invalidate();
            }
        }

        private string _Text = "STNCTRL";

        public string Text {
            get { return _Text; }
            set {
                _Text = value;
                this.Invalidate();
            }
        }

        private Font _Font;

        public Font Font {
            get { return _Font; }
            set {
                if (value == _Font) return;
                if (value == null) throw new ArgumentNullException("值不能为空");
                _Font = value;
                this.Invalidate();
            }
        }

        protected StringFormat m_sf;

        public STNodeControl() {
            m_sf = new StringFormat();
            m_sf.Alignment = StringAlignment.Center;
            m_sf.LineAlignment = StringAlignment.Center;
            this._Font = new Font("courier new", 8.25f);
            this.Width = 75;
            this.Height = 23;
        }

        protected internal virtual void OnPaint(DrawingTools dt) {
            Graphics g = dt.Graphics;
            SolidBrush brush = dt.SolidBrush;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            brush.Color = this._BackColor;
            g.FillRectangle(brush, 0, 0, this.Width, this.Height);
            if (!string.IsNullOrEmpty(this._Text)) {
                brush.Color = this._ForeColor;
                g.DrawString(this._Text, this._Font, brush, this.ClientRectangle, m_sf);
            }
        }

        public void Invalidate() {
            if (this._Owner == null) return;
            this._Owner.Invalidate(new Rectangle(this._Left, this._Top + this._Owner.TitleHeight, this.Width, this.Height));
        }

        public void Invalidate(Rectangle rect) {
            if (this._Owner == null) return;
            this._Owner.Invalidate(this.RectangleToParent(rect));
        }

        public Rectangle RectangleToParent(Rectangle rect) {
            return new Rectangle(this._Left, this._Top + this._Owner.TitleHeight, this.Width, this.Height);
        }

        public event EventHandler GotFocus;
        public event EventHandler LostFocus;
        public event EventHandler MouseEnter;
        public event EventHandler MouseLeave;
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseUp;
        public event MouseEventHandler MouseClick;
        public event MouseEventHandler MouseWheel;
        public event EventHandler MouseHWheel;

        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;
        public event KeyPressEventHandler KeyPress;

        public event EventHandler Move;
        public event EventHandler Resize;


        protected internal virtual void OnGotFocus(EventArgs e) {
            if (this.GotFocus != null) this.GotFocus(this, e);
        }
        protected internal virtual void OnLostFocus(EventArgs e) {
            if (this.LostFocus != null) this.LostFocus(this, e);
        }
        protected internal virtual void OnMouseEnter(EventArgs e) {
            if (this.MouseEnter != null) this.MouseEnter(this, e);
        }
        protected internal virtual void OnMouseLeave(EventArgs e) {
            if (this.MouseLeave != null) this.MouseLeave(this, e);
        }
        protected internal virtual void OnMouseDown(MouseEventArgs e) {
            if (this.MouseDown != null) this.MouseDown(this, e);
        }
        protected internal virtual void OnMouseMove(MouseEventArgs e) {
            if (this.MouseMove != null) this.MouseMove(this, e);
        }
        protected internal virtual void OnMouseUp(MouseEventArgs e) {
            if (this.MouseUp != null) this.MouseUp(this, e);
        }
        protected internal virtual void OnMouseClick(MouseEventArgs e) {
            if (this.MouseClick != null) this.MouseClick(this, e);
        }
        protected internal virtual void OnMouseWheel(MouseEventArgs e) {
            if (this.MouseWheel != null) this.MouseWheel(this, e);
        }
        protected internal virtual void OnMouseHWheel(MouseEventArgs e) {
            if (this.MouseHWheel != null) this.MouseHWheel(this, e);
        }

        protected internal virtual void OnKeyDown(KeyEventArgs e) {
            if (this.KeyDown != null) this.KeyDown(this, e);
        }
        protected internal virtual void OnKeyUp(KeyEventArgs e) {
            if (this.KeyUp != null) this.KeyUp(this, e);
        }
        protected internal virtual void OnKeyPress(KeyPressEventArgs e) {
            if (this.KeyPress != null) this.KeyPress(this, e);
        }

        protected internal virtual void OnMove(EventArgs e) {
            if (this.Move != null) this.Move(this, e);
        }

        protected internal virtual void OnResize(EventArgs e) {
            if (this.Resize != null) this.Resize(this, e);
        }

        public IAsyncResult BeginInvoke(Delegate method) { return this.BeginInvoke(method, null); }
        public IAsyncResult BeginInvoke(Delegate method, params object[] args) {
            if (this._Owner == null) return null;
            return this._Owner.BeginInvoke(method, args);
        }
        public object Invoke(Delegate method) { return this.Invoke(method, null); }
        public object Invoke(Delegate method, params object[] args) {
            if (this._Owner == null) return null;
            return this._Owner.Invoke(method, args);
        }
    }
}