using System;
using System.Collections.Generic;

namespace ApplicationLogic.Interfaces
{
    public interface IApplicationUI
    {
        int GetSelectedModelIndex();
        void SendMessage(string msg);
        void ShowAvailableModels(List<string> models);
        void UpdateStatusBarInfo(string info);
        void AddCamera(string cameraName);
        void RemoveCamera(string cameraName);
        void Close();
        void LogMessage(string msg);
    }
}
