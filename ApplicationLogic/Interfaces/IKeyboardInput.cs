using System.Windows.Forms;

namespace ApplicationLogic.Interfaces
{
    public interface IKeyboardInput
    {
        void KeyPress(KeyPressEventArgs e);
        void KeyDown(KeyEventArgs e);
        void KeyUp(KeyEventArgs e);
    }
}
