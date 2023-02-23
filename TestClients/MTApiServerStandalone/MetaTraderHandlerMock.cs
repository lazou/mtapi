using MTApiService;

namespace MTApiServerStandalone
{
    internal class MetaTraderHandlerMock : IMetaTraderHandler
    {
        public void SendTickToMetaTrader(int handle)
        {
            // this will be called by mql5 via MT5Connector.cpp to dequeue the task
            var commandType = MtAdapter.GetInstance().GetCommandType(handle);

            // this will be called by mql5 via MT5Connector.cpp to send the response based on the given commandtype
            if (commandType == 155) // Mt5CommandType.MtRequest
            {
                // this will be called by mql5 via MT5Connector.cpp to get the request which should be executed via mql5 against the broker
                var requestPayload = MtAdapter.GetInstance().GetCommandParameter<string>(handle, 0);
                
                // return OrderSendResult
                var responsePayload = "{\"ErrorCode\" : \"0\",\"Value\" : {\"RetVal\" : true,\"TradeResult\" : {\"Deal\" : 0,\"Price\" : 123.0,\"Ask\" : 0,\"Retcode\" : 10009,\"Volume\" : 0,\"Comment\" : \"Request executed\",\"Order\" : 0,\"Request_id\" : 120,\"Bid\" : 0}}}";
                MtAdapter.GetInstance().SendResponse(handle, new MtResponseString(responsePayload));
            }
            else
            {
                MtAdapter.GetInstance().SendResponse(handle, new MtResponseString($"just a description for command type {commandType}"));
            }
        }
    }
}
