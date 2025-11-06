namespace LecX.Application.Features.Categories.GetCategoryById
{
    public class GetCategoryByIdResponse
    {
        public int CategoryId { get; set; }
        public string FullName { get; set; }
        public string? Description { get; set; }
    }
}