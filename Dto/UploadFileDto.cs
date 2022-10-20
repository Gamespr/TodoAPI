namespace TodoApi.Dto
{
    public class UploadFileDto
    {
        public Guid UploadFileId { get; set; }
        public string Name { get; set; } = null!;
        public string Src { get; set; } = null!;
        public Guid TodoId { get; set; }

    }
}
