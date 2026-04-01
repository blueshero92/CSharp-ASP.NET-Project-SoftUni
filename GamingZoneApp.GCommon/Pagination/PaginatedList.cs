namespace GamingZoneApp.GCommon.Pagination
{
    //Paginated list is made generic to be able to use it for any type of data we want to paginate, such as developers, publishers or games.
    public class PaginatedList<T> : List<T>
    {
        // The current page index (1-based).
        public int PageIndex { get; private set; }

        // The total number of pages based on the total item count and page size.
        public int TotalPages { get; private set; }

        // Constructor to initialize the paginated list with items, total count, page index, and page size.
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        // Indicates if there is a previous page available.
        public bool HasPreviousPage => PageIndex > 1;

        // Indicates if there is a next page available.
        public bool HasNextPage => PageIndex < TotalPages;

        // Static method to create a paginated list asynchronously from an enumerable source.
        public static async Task<PaginatedList<T>> CreateAsync(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            // Calculate the total count of items in the source.
            int count = source.Count();

            // Retrieve the items for the current page by skipping the appropriate number of items and taking the page size.
            List<T> items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(new PaginatedList<T>(items, count, pageIndex, pageSize));
        }
    }
}
