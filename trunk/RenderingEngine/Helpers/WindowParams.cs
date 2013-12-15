using System;

namespace RenderingEngine.Helpers
{
    public struct WindowParams
    {
        public IntPtr Handle;
        public string Name;
        public uint Width;
        public uint Height;
        public uint ColorDepth;
    }
}
