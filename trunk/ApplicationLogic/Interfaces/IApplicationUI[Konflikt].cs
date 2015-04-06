using System.Collections.Generic;
using System.Drawing;
using ApplicationLogic.Scene;

namespace ApplicationLogic.Interfaces
{
    public interface IApplicationUI
    {
        string GetSelectedModelName();
        void SendMessage(string msg);
        void ShowAvailableModels(List<string> models);
        void UpdateStatusBarInfo(string info);
        void ModelAdded(ModelProperties modelProperties);
        void ModelSelected(ModelProperties modelProperties);
        void ModelRemoved(string modelName);
        void CameraAdded(SecurityCameraProperties cameraProperties);
        void CameraRemoved(string cameraName);
        void CameraSelected(SecurityCameraProperties cameraProperties);
        void UpdateCameraProperties(SecurityCameraProperties cameraProperties);
        Size GetCameraPreviewDimension();
        void UpdateCameraView(string cameraName, Bitmap bmp);
        void Close();
        void LogMessage(string msg);
    }
}
