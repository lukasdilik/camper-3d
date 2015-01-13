using System.Windows.Forms;

namespace ApplicationLogic.Interfaces
{
    public interface IMouseInput
    {
        void MouseUp(MouseEventArgs e);
        void MouseDown(MouseEventArgs e);
        void MouseMove(MouseEventArgs e);
        void MouseDoubleClick(MouseEventArgs e);
    }
}
