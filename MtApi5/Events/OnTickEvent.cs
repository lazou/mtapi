namespace MtApi5.Events
{
    public class OnTickEvent
    {
        public MqlTick Tick { get; set; }
        public string Instrument { get; set; }
        public int ExpertHandle { get; set; }
    }
}