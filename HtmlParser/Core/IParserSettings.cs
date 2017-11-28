

namespace HtmlParser.Core
{
    interface IParserSettings /*Настройки парсера*/
    {
        string BaseUrl { get; set; }

        string Prefix { get; set; }

        int StartPoint { get; set; }

        int EndPoint { get; set; }
    } 
}
