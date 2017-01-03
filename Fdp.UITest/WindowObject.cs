using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;

namespace Fdp.UITest
{
    public class WindowObject
    {
        protected Window window;

        protected WindowObject(Window window)
        {
            this.window = window;
        }

        protected Button Button(string title)
        {
            return window.Get<Button>(title);
        }

        protected CheckBox CheckBox(string title)
        {
            return window.Get<CheckBox>(title);
        }
    }
}