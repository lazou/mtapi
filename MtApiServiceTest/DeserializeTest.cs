using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;
using MtApi5.Events;
using System.Text.Json.Serialization;
using static MtApiServiceNetCore.JsonConverter;

namespace MtApiServiceTest
{
    [TestClass]
    public class DeserializeTest
    {
        [TestMethod]
        public void TestOnTickEvent()
        {
            // arrange
            var deserializeOptions = new JsonSerializerOptions { Converters = { new Int64JsonConverter(), new UInt64JsonConverter() } };
            var payload = "{\"ExpertHandle\" : 0,\"Instrument\" : \"[ANYSYMBOL]\",\"Tick\" : {\"bid\" : 12884.82,\"MtTime\" : 1658106300.0,\"last\" : 12884.82,\"ask\" : 12893.32,\"volume\" : 187.0,\"volume_real\" : 213.44}}";
            
            // act
            var e = JsonSerializer.Deserialize<OnTickEvent>(payload, deserializeOptions);

            // assert
            Assert.AreEqual(0, e.ExpertHandle);
            Assert.AreEqual("[ANYSYMBOL]", e.Instrument);
            Assert.AreEqual(12884.82, e.Tick.bid);
            Assert.AreEqual(1658106300, e.Tick.MtTime);
            Assert.AreEqual(12884.82, e.Tick.last);
            Assert.AreEqual(12893.32, e.Tick.ask);
            Assert.AreEqual(187u, e.Tick.volume);
            Assert.AreEqual(213.44, e.Tick.volume_real);
        }

        [TestMethod]
        public void TestOnTickEventWithoutConverter()
        {
            // arrange
            var deserializeOptions = new JsonSerializerOptions();
            var payload = "{\"ExpertHandle\" : 0,\"Instrument\" : \"[ANYSYMBOL]\",\"Tick\" : {\"bid\" : 12884.82,\"MtTime\" : 1658106300.0,\"last\" : 12884.82,\"ask\" : 12893.32,\"volume\" : 187.0,\"volume_real\" : 213.44}}";
            
            // act
            Assert.ThrowsException<JsonException>(()=>JsonSerializer.Deserialize<OnTickEvent>(payload, deserializeOptions), "time can not be converted from double to int64 without custom converter");
        }

        [TestMethod]
        public void TestOnTickEventWithLongTime()
        {
            // arrange
            var deserializeOptions = new JsonSerializerOptions { Converters = { new UInt64JsonConverter() } };
            var payload = "{\"ExpertHandle\" : 0,\"Instrument\" : \"[ANYSYMBOL]\",\"Tick\" : {\"bid\" : 12884.82,\"MtTime\" : 1658106300,\"last\" : 12884.82,\"ask\" : 12893.32,\"volume\" : 187.0,\"volume_real\" : 213.44}}";

            // act
            var e = JsonSerializer.Deserialize<OnTickEvent>(payload, deserializeOptions);

            // assert
            Assert.AreEqual(0, e.ExpertHandle);
            Assert.AreEqual("[ANYSYMBOL]", e.Instrument);
            Assert.AreEqual(12884.82, e.Tick.bid);
            Assert.AreEqual(1658106300, e.Tick.MtTime);
            Assert.AreEqual(12884.82, e.Tick.last);
            Assert.AreEqual(12893.32, e.Tick.ask);
            Assert.AreEqual(187u, e.Tick.volume);
            Assert.AreEqual(213.44, e.Tick.volume_real);
        }

        [TestMethod]
        public void TestOnTickEventWithoutConverterWithLongTime()
        {
            // arrange
            var deserializeOptions = new JsonSerializerOptions();
            var payload = "{\"ExpertHandle\" : 0,\"Instrument\" : \"[ANYSYMBOL]\",\"Tick\" : {\"bid\" : 12884.82,\"MtTime\" : 1658106300,\"last\" : 12884.82,\"ask\" : 12893.32,\"volume\" : 187.0,\"volume_real\" : 213.44}}";

            // act
            Assert.ThrowsException<JsonException>(() => JsonSerializer.Deserialize<OnTickEvent>(payload, deserializeOptions), "volume can not be converted from double to uint64 without custom converter");
        }
    }
}
