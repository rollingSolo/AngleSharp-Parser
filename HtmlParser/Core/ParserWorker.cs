﻿
using System;



namespace HtmlParser.Core
{
    class ParserWorker<T> where T : class
    {
        IParser<T> parser;

        IParserSettings parserSettings;

        HtmlLoader loader;

        bool isActive;

        #region Properties

        public event Action<object, T> OneNewData;
        public event Action<object> OneCompleted;
        public IParser<T> Parser
        {
            get
            {
                return parser;
            }

            set
            {
                parser = value;
            }
        }

        public IParserSettings Settings
        {
            get
            {
                return parserSettings;
            }

            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }
        #endregion

        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;

        }

        public ParserWorker(IParser<T> parser,IParserSettings parserSettings) : this(parser)
        {
            this.parserSettings = parserSettings;
        }

        public void Start()
        {
            isActive = true;
            Worker();
        }

        public void Abort()
        {
            isActive = false;
        }

        private async void Worker()
        {
            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {
                if (!isActive)
                {
                    OneCompleted?.Invoke(this);
                    return;
                }

                var source = await loader.GetSourceByPageId(i);

                var domParser = new AngleSharp.Parser.Html.HtmlParser();

                var document = await domParser.ParseAsync(source);

                var result = parser.Parse(document);

                OneNewData?.Invoke(this, result);
            }

            OneCompleted?.Invoke(this);
            isActive = false;
        }
    }
}
