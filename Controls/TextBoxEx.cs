using System;
using System.Drawing;
using System.Windows.Forms;

namespace GpsConverter.Controls
{
    public class TextBoxEx : TextBox
    {
        private bool _error;
        private Color _defaultBackColor;

        public bool Error
        {
            get { return _error; }
            set
            {
                if (_error == value)
                    return;
                _error = value;
                if (_error)
                {
                    _defaultBackColor = BackColor;
                    BackColor = Color.Red;
                }
                else
                    BackColor = _defaultBackColor;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                SelectAll();
                e.SuppressKeyPress = true;
            }
            base.OnKeyDown(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (Error)
                Error = false;
        }
    }
}