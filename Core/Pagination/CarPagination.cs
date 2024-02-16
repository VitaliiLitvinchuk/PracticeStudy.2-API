namespace Core.Pagination
{
    public class CarPagination
    {
        public string? BrandName { get; set; }
        public int? Year { get; set; }
        public required int Page { get; set; } = 1;
        public required int PageSize { get; set; } = 15;
    }
}
