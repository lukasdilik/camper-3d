using System.Windows.Forms;

namespace ApplicationLogic.Interfaces
{
    public interface IKeyboardInput
    {
        void KeyPress(char keyChar);
        void KeyDown(Keys key);
        void KeyUp(Keys key);
    }
}
