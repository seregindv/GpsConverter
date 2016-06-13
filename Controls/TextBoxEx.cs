using System.Windows.Forms;

namespace GpsConverter.Controls
{
    public class TextBoxEx : TextBox
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                SelectAll();
                e.SuppressKeyPress = true;
            }
            base.OnKeyDown(e);
        }
    }
}