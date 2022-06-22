using System;

namespace XamForm.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
    public class RpModels
    {
        public int code { get; set; } = 200;
        public string message { get; set; } = string.Empty;
        public object data { get; set; }
        public object fieldsinfo { get; set; }
    }
}