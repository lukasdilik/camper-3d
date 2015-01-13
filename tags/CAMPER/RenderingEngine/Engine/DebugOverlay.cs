using Mogre;

namespace RenderingEngine.Engine
{
    public class DebugOverlay
    {
        protected RenderWindow Window;
        protected OverlayElement GuiAvg;
        protected OverlayElement GuiCurr;
        protected OverlayElement GuiBest;
        protected OverlayElement GuiWorst;
        protected OverlayElement GuiTriangleCount;
        protected OverlayElement ModesText;
        
        private string mAdditionalInfo = "";

        public DebugOverlay(RenderWindow window)
        {
            Window = window;

            var debugOverlay = OverlayManager.Singleton.GetByName("Core/DebugOverlay");
            debugOverlay.Show();

            GuiAvg = OverlayManager.Singleton.GetOverlayElement("Core/AverageFps");
            GuiCurr = OverlayManager.Singleton.GetOverlayElement("Core/CurrFps");
            GuiBest = OverlayManager.Singleton.GetOverlayElement("Core/BestFps");
            GuiWorst = OverlayManager.Singleton.GetOverlayElement("Core/WorstFps");
            GuiTriangleCount = OverlayManager.Singleton.GetOverlayElement("Core/NumTris");
            ModesText = OverlayManager.Singleton.GetOverlayElement("Core/NumBatches");
        }

        public string AdditionalInfo
        {
            set { mAdditionalInfo = value; }
            get { return mAdditionalInfo; }
        }

        public void Update(float timeFragment)
        {
            var stats = Window.GetStatistics();

            GuiAvg.Caption = "Average FPS: " + stats.AvgFPS;
            GuiCurr.Caption = "Current FPS: " + stats.LastFPS;
            GuiBest.Caption = "Best FPS: " + stats.BestFPS + " " + stats.BestFrameTime + " ms";
            GuiWorst.Caption = "Worst FPS: " + stats.WorstFPS + " " + stats.WorstFrameTime + " ms";
            GuiTriangleCount.Caption = "Triangle Count: " + stats.TriangleCount;
            ModesText.Caption = mAdditionalInfo;
        }
    }
}
