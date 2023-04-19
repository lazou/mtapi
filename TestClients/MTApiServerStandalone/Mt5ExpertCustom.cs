using MTApiService;

namespace MTApiServerStandalone
{
    internal class Mt5ExpertCustom : Mt5Expert
    {
        public Mt5ExpertCustom(int handle, string symbol, double bid, double ask, bool isTestMode) : base(handle, symbol, bid, ask, isTestMode)
        {
        }

        protected override void NotifyCommandReady()
        {
            // this will be called by mql5 via MT5Connector.cs to dequeue the task
            var commandType = MtAdapter.GetInstance().GetCommandType(Handle);

            // this will be called by mql5 via MT5Connector.cs to send the response based on the given commandtype
            if (commandType == 155) // Mt5CommandType.MtRequest
            {
                // this will be called by mql5 via MT5Connector.cs to get the request which should be executed via mql5 against the broker
                var requestPayload = MtAdapter.GetInstance().GetCommandParameter<string>(Handle, 0);
                
                // return OrderSendResult
                var responsePayload = "{\"ErrorCode\" : 0,\"Value\" : {\"RetVal\" : true,\"TradeResult\" : {\"Deal\" : 0,\"Price\" : 123.0,\"Ask\" : 0,\"Retcode\" : 10009,\"Volume\" : 0,\"Comment\" : \"Request executed\",\"Order\" : 0,\"Request_id\" : 120,\"Bid\" : 0}}}";
                MtAdapter.GetInstance().SendResponse(Handle, new MtResponseString(responsePayload));
            }
            else
            {
                MtAdapter.GetInstance().SendResponse(Handle, new MtResponseString($"just a description for command type {commandType}"));
            }
        }
    }
}
