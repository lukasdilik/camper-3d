using System.Collections.Generic;
using System.Drawing;
using ApplicationLogic.Scene;

namespace ApplicationLogic.Interfaces
{
    public interface IApplicationUI
    {
        int GetSelectedModelIndex();
        void SendMessage(string msg);
        void ShowAvailableModels(List<string> models);
        void UpdateStatusBarInfo(string info);
        void AddCamera(SecurityCameraProperties cameraProperties);
        void RemoveCamera(string cameraName);
        void CameraSelected(SecurityCameraProperties cameraProperties);
        void UpdateCameraProperties(SecurityCameraProperties cameraProperties);
        Size GetCameraPreviewDimension();
        void UpdateCameraView(string cameraName, Bitmap bmp);
        void Close();
        void LogMessage(string msg);
    }
}
