namespace LittleBitGames.Ads.AdUnits
{
    public class AdErrorInfo : IAdErrorInfo
    {
        public AdErrorInfo(MaxSdkBase.ErrorInfo errorInfo)
        {
            Message = errorInfo.Message;
            MediatedNetworkErrorMessage = errorInfo.MediatedNetworkErrorMessage;
            MediatedNetworkErrorCode = errorInfo.MediatedNetworkErrorCode;
        }
        
        public string Message { get; private set; }
        public int MediatedNetworkErrorCode { get; private set; }
        public string MediatedNetworkErrorMessage { get; private set; }
    }
}