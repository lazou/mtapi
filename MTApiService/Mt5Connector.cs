using System;


namespace MTApiService
{

    public class Mt5Connector
    {
        private static bool Execute(Action func, ref string err)
        {
            try
            {
                func();
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                MtAdapter.GetInstance().LogError(err);
                return false;
            }
        }
        private static bool ExecuteGetCommandParameter<T>(int expertHandle, int paramIndex, ref T res, ref string err)
        {
            try
            {
                res = MtAdapter.GetInstance().GetCommandParameter<T>(expertHandle, paramIndex);
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                MtAdapter.GetInstance().LogError(err);
                return false;
            }
        }

        public static bool InitExpert(int expertHandle, int port, string symbol, double bid, double ask, int isTesting, ref string err)
        {
            return Execute(() =>
            {
                var expert = new Mt5Expert(expertHandle, symbol, bid, ask, isTesting != 0);
                MtAdapter.GetInstance().AddExpert(port, expert);
            }, ref err);
        }

        public static bool DeinitExpert(int expertHandle, ref string err)
        {
            return Execute(() =>
            {
                MtAdapter.GetInstance().RemoveExpert(expertHandle);
            }, ref err);
        }

        // not used in MtApi5.mq5
        public static bool UpdateQuote(int expertHandle, string symbol, double bid, double ask, ref string err)
        {
            return Execute(() =>
            {
                MtAdapter.GetInstance().SendQuote(expertHandle, symbol, bid, ask);
            }, ref err);
        }

        public static bool SendEvent(int expertHandle, int eventType, string payload, ref string err)
        {
            return Execute(() =>
            {
                MtAdapter.GetInstance().SendEvent(expertHandle, eventType, payload);
            }, ref err);
        }

        public static bool SendIntResponse(int expertHandle, int response, ref string err)
        {
            return Execute(() =>
            {
                MtAdapter.GetInstance().SendResponse(expertHandle, new MtResponseInt(response));
            }, ref err);
        }

        public static bool SendLongResponse(int expertHandle, long response, ref string err)
        {
            return Execute(() =>
            {
                MtAdapter.GetInstance().SendResponse(expertHandle, new MtResponseLong(response));
            }, ref err);
        }

        public static bool SendULongResponse(int expertHandle, ulong response, ref string err)
        {
            return Execute(() =>
            {
                MtAdapter.GetInstance().SendResponse(expertHandle, new MtResponseULong(response));
            }, ref err);
        }

        public static bool SendBooleanResponse(int expertHandle, int response, ref string err)
        {
            return Execute(() =>
            {
                bool value = (response != 0) ? true : false;
                MtAdapter.GetInstance().SendResponse(expertHandle, new MtResponseBool(value));
            }, ref err);
        }

        public static bool SendDoubleResponse(int expertHandle, double response, ref string err)
        {
            return Execute(() =>
            {
                MtAdapter.GetInstance().SendResponse(expertHandle, new MtResponseDouble(response));
            }, ref err);
        }

        public static bool SendStringResponse(int expertHandle, string response, ref string err)
        {
            return Execute(() =>
            {
                MtAdapter.GetInstance().SendResponse(expertHandle, new MtResponseString(response));
            }, ref err);
        }

        public static bool SendVoidResponse(int expertHandle, ref string err)
        {
            return Execute(() =>
            {
                MtAdapter.GetInstance().SendResponse(expertHandle, new MtResponseObject(null));
            }, ref err);
        }

        public static bool SendDoubleArrayResponse(int expertHandle, double[] values, int size, ref string err)
        {
            return Execute(() =>
            {
                MtAdapter.GetInstance().SendResponse(expertHandle, new MtResponseDoubleArray(values));
            }, ref err);
        }

        public static bool SendIntArrayResponse(int expertHandle, int[] values, int size, ref string err)
        {
            return Execute(() =>
            {
                MtAdapter.GetInstance().SendResponse(expertHandle, new MtResponseIntArray(values));
            }, ref err);
        }

        public static bool SendLongArrayResponse(int expertHandle, long[] values, int size, ref string err)
        {
            return Execute(() =>
            {
                MtAdapter.GetInstance().SendResponse(expertHandle, new MtResponseLongArray(values));
            }, ref err);
        }

        public static bool SendMqlRatesArrayResponse(int expertHandle, string values, int size, ref string err)
        {
            return Execute(() =>
            {
                MtAdapter.GetInstance().SendResponse(expertHandle, new MtResponseMqlRatesArray(values));
            }, ref err);
        }

        public static bool SendErrorResponse(int expertHandle, int code, string message, ref string err)
        {
            return Execute(() =>
            {
                var res = new MtResponseString(message);
                res.ErrorCode = code;
                MtAdapter.GetInstance().SendResponse(expertHandle, res);
            }, ref err);
        }

        //----------- get values -------------------------------

        public static bool GetCommandType(int expertHandle, ref int res, ref string err)
        {
            try
            {
                res = MtAdapter.GetInstance().GetCommandType(expertHandle);
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                MtAdapter.GetInstance().LogError(err);
                return false;
            }
        }

        public static bool GetIntValue(int expertHandle, int paramIndex, ref int res, ref string err)
        {
            return ExecuteGetCommandParameter(expertHandle, paramIndex, ref res, ref err);
        }

        public static bool GetDoubleValue(int expertHandle, int paramIndex, ref double res, ref string err)
        {
            return ExecuteGetCommandParameter(expertHandle, paramIndex, ref res, ref err);
        }

        public static bool GetStringValue(int expertHandle, int paramIndex, ref string res, ref string err)
        {
            return ExecuteGetCommandParameter(expertHandle, paramIndex, ref res, ref err);
        }

        public static bool GetULongValue(int expertHandle, int paramIndex, ref ulong res, ref string err)
        {
            return ExecuteGetCommandParameter(expertHandle, paramIndex, ref res, ref err);
        }

        public static bool GetLongValue(int expertHandle, int paramIndex, ref long res, ref string err)
        {
            return ExecuteGetCommandParameter(expertHandle, paramIndex, ref res, ref err);
        }

        public static bool GetBooleanValue(int expertHandle, int paramIndex, ref bool res, ref string err)
        {
            return ExecuteGetCommandParameter(expertHandle, paramIndex, ref res, ref err);
        }

        public static bool GetUIntValue(int expertHandle, int paramIndex, ref uint res, ref string err)
        {
            return ExecuteGetCommandParameter(expertHandle, paramIndex, ref res, ref err);
        }
    }
}
