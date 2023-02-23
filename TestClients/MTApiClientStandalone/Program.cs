// See https://aka.ms/new-console-template for more information
using MtApi5;
using MTApiService;

Console.WriteLine("Connecting to server");
var client = new MtApi5Client();
client.ConnectionStateChanged += Client_ConnectionStateChanged;
client.QuoteUpdate += Client_QuoteUpdate;

void Client_QuoteUpdate(object? sender, Mt5QuoteEventArgs e)
{
    Console.WriteLine($"Quote received: {e.Quote.Instrument}-{e.Quote.Ask}-{e.Quote.Bid}");
}

void Client_ConnectionStateChanged(object? sender, Mt5ConnectionEventArgs e)
{
    if (e.Status == Mt5ConnectionState.Connected)
    {
        var quotes = client.GetQuotes();
        foreach (var quote in quotes)
        {
            Console.WriteLine($"{quote.Instrument}-{quote.Ask}-{quote.Bid}-{quote.ExpertHandle}");
        }
        Console.WriteLine($"SYMBOL_DESCRIPTION: {client.SymbolInfoString(quotes.First().Instrument, ENUM_SYMBOL_INFO_STRING.SYMBOL_DESCRIPTION)}");

        Console.WriteLine("Executing a request with payload (OrderSend)");
        MqlTradeResult result;
        if (client.OrderSend(new MqlTradeRequest {Action=ENUM_TRADE_REQUEST_ACTIONS.TRADE_ACTION_DEAL, Price=1234.5, Volume=0.1, Magic=3 }, out result))
        {
            Console.WriteLine($"Request was accepted: {result}");
        }
        else
        {
            Console.WriteLine($"Request was rejected: {result}");
        }
    }
}

client.BeginConnect(8222);
Console.ReadLine();