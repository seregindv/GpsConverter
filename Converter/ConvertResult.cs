namespace GpsConverter.Converter
{
    public class ConvertResult
    {
        private bool _isError;
        private string _title;
        private string _text;

        public ConvertResult(string title, string text, bool isError = false)
        {
            _isError = isError;
            _title = title;
            _text = text;
        }

        public bool IsError
        {
            get { return _isError; }
        }

        public string Title
        {
            get { return _title; }
        }

        public string Text
        {
            get { return _text; }
        }
    }
}