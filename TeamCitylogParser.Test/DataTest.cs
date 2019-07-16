using System;
using Xunit;
using static System.String;


namespace TeamCityLogParser.Test
{
    public class DataTest
    {
        [Fact]
        public void GivenText_ShouldProduceValidObjectOrThrow()
        {
            var text = "one" + Environment.NewLine + "two";
            var data = new DataService(text);
            
            Assert.Equal("one", data.Data(1));
            Assert.Equal("two", data.Data(2));
            Assert.Equal(Empty, data.Data(3));
            Assert.Throws<NullReferenceException>(() => { new DataService(null);});
            Assert.Throws<NullReferenceException>(() => { new DataService(Empty);});
        }
    }
}
