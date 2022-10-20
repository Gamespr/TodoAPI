using TodoApi.Models;

namespace TodoApi.Dto
{
    public class TodoListSelectDto
    {
        public Guid TodoId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Enable { get; set; }
        public int Orders { get; set; }
        public string InsertEmployeeName { get; set; }
        public string UpdateEmployeeName { get; set; }
        public ICollection<UploadFileDto> UploadFiles { get; set; }
    }
}
