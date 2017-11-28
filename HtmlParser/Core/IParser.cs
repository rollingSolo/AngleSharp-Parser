using AngleSharp.Dom.Html;


namespace HtmlParser.Core
{
    interface IParser<T> where T : class 
    {
        //Обобщенный интерфейс - классы которые его будут реализовывать, 
        //смогут возвращать данные любого ссылочного типа

        T Parse(IHtmlDocument document);
    }
}
