using System.Text.RegularExpressions;

namespace TodoApi.Parameters
{
    public class TodoSelectParameter
    {
        public string? name { get; set; }
        public bool? enable { get; set; }
        public DateTime? InsertTime { get; set; }
        public int? minOrder { get; set; }
        public int? maxOrder { get; set; }

        private string _order;

        public string? Order
        {
            get { return _order; }
            set
            {
                //EX:2-4
                Regex regex = new Regex(@"^\d*-\d$");
                if (regex.IsMatch(value))
                {
                    minOrder = Int32.Parse(value.Split('-')[0]);
                    maxOrder = Int32.Parse(value.Split('-')[0]);
                }

                _order = value;
            }
        }
    }
}
