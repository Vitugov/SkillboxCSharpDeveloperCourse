using Newtonsoft.Json;

namespace JsonSerializationTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dic = new Dictionary<SomeClass, int> { [new SomeClass("one")] = 1 };
            string json = JsonConvert.SerializeObject(dic, Formatting.Indented);
            Dictionary<SomeClass, int> deserializedDic = JsonConvert.DeserializeObject<Dictionary<SomeClass, int>>(json);
        }
    }
}
