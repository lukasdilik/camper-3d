using System;
using System.Collections.Generic;

namespace ApplicationLogic.Interfaces
{
    public interface IApplicationUI
    {
        void SendMessage(string msg);
        void ShowAvailableModels(List<string> models);
        void UpdateCameraInformation(string info);
        void ExceptionOccured(Exception e);
    }
}
