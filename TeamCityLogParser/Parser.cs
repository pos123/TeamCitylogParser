namespace TeamCityLogParser
{
    public class Parser
    {
        private readonly string text;

        public Parser(string text)
        {
            this.text = text;
        }

        public void Run()
        {
            var data = new DataService(text);
            foreach (var line in data.GetNextLine())
            {
                // use the extractor to determine the IEntry
                // store and create the IEntry type in the correct location
            }
        }
        
    }
}
